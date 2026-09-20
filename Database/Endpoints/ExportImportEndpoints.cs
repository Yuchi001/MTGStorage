using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MTGStorage.Database.Endpoints
{
    public static class ExportImportEndpoints
    {
        private sealed class CsvTable
        {
            public string[] Headers { get; set; }
            public List<string[]> Rows { get; set; }
        }

        private sealed class TableColumn
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsRequired { get; set; }
        }

        /// <summary>
        /// Exports the Location, Card and CardFace tables to CSV files in a ZIP archive.
        /// </summary>
        /// <returns>True when the archive was created; false when the user cancels the dialog.</returns>
        public static bool ExportToCSV()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "ZIP archive (*.zip)|*.zip";
                saveFileDialog.DefaultExt = "zip";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = "MTGStorage-export.zip";
                saveFileDialog.Title = "Save CSV export";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    // A single read transaction keeps all three CSV files consistent.
                    using (var transaction = connection.BeginTransaction())
                    using (var archive = ZipFile.Open(saveFileDialog.FileName, ZipArchiveMode.Create))
                    {
                        ExportTable(connection, transaction, archive, "Location", "Locations.csv");
                        ExportTable(connection, transaction, archive, "Card", "Cards.csv");
                        ExportTable(connection, transaction, archive, "CardFace", "CardFaces.csv");

                        transaction.Commit();
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Validates a ZIP export and replaces the current database contents with its data.
        /// </summary>
        /// <returns>True when the data was imported; false when the user cancels an input dialog.</returns>
        public static bool ImportFromCSV()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ZIP archive (*.zip)|*.zip";
                openFileDialog.DefaultExt = "zip";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Select CSV export to import";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    CsvTable locations;
                    CsvTable cards;
                    CsvTable cardFaces;

                    using (var archive = ZipFile.OpenRead(openFileDialog.FileName))
                    {
                        locations = ReadCsvEntry(archive, "Locations.csv");
                        cards = ReadCsvEntry(archive, "Cards.csv");
                        cardFaces = ReadCsvEntry(archive, "CardFaces.csv");
                    }

                    var locationColumns = GetTableColumns(connection, "Location");
                    var cardColumns = GetTableColumns(connection, "Card");
                    var cardFaceColumns = GetTableColumns(connection, "CardFace");

                    ValidateTable(locations, locationColumns, "Locations.csv");
                    ValidateTable(cards, cardColumns, "Cards.csv");
                    ValidateTable(cardFaces, cardFaceColumns, "CardFaces.csv");
                    ValidateRelationships(locations, cards, cardFaces);

                    if (MessageBox.Show(
                            "Importing will replace all current locations and cards. Continue?",
                            "Confirm CSV Import",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        return false;
                    }

                    using (var transaction = connection.BeginTransaction())
                    {
                        DeleteExistingData(connection, transaction);
                        InsertTable(connection, transaction, "Location", locationColumns, locations);
                        InsertTable(connection, transaction, "Card", cardColumns, cards);
                        InsertTable(connection, transaction, "CardFace", cardFaceColumns, cardFaces);
                        transaction.Commit();
                    }
                }

                return true;
            }
        }

        private static CsvTable ReadCsvEntry(ZipArchive archive, string fileName)
        {
            var entries = archive.Entries
                .Where(entry => string.Equals(entry.FullName, fileName, StringComparison.Ordinal))
                .ToList();

            if (entries.Count != 1)
            {
                throw new InvalidDataException(
                    "The archive must contain exactly one " + fileName + " file.");
            }

            using (var stream = entries[0].Open())
            using (var reader = new StreamReader(stream, Encoding.UTF8, true))
            {
                var records = ParseCsv(reader.ReadToEnd(), fileName);
                if (records.Count == 0)
                {
                    throw new InvalidDataException(fileName + " does not contain a header row.");
                }

                return new CsvTable
                {
                    Headers = records[0],
                    Rows = records.Skip(1).ToList()
                };
            }
        }

        private static List<string[]> ParseCsv(string content, string fileName)
        {
            var records = new List<string[]>();
            var record = new List<string>();
            var field = new StringBuilder();
            var isQuoted = false;
            var quoteClosed = false;
            var hasPendingField = false;

            for (var index = 0; index < content.Length; index++)
            {
                var character = content[index];

                if (isQuoted)
                {
                    if (character == '"')
                    {
                        if (index + 1 < content.Length && content[index + 1] == '"')
                        {
                            field.Append('"');
                            index++;
                        }
                        else
                        {
                            isQuoted = false;
                            quoteClosed = true;
                        }
                    }
                    else
                    {
                        field.Append(character);
                    }

                    continue;
                }

                if (quoteClosed && character != ',' && character != '\r' && character != '\n')
                {
                    throw new InvalidDataException(
                        "Invalid quoted field in " + fileName + ".");
                }

                if (character == ',')
                {
                    record.Add(field.ToString());
                    field.Clear();
                    quoteClosed = false;
                    hasPendingField = true;
                    continue;
                }

                if (character == '\r' || character == '\n')
                {
                    if (character == '\r' && index + 1 < content.Length && content[index + 1] == '\n')
                    {
                        index++;
                    }

                    record.Add(field.ToString());
                    records.Add(record.ToArray());
                    record.Clear();
                    field.Clear();
                    quoteClosed = false;
                    hasPendingField = false;
                    continue;
                }

                if (character == '"')
                {
                    if (field.Length != 0)
                    {
                        throw new InvalidDataException(
                            "Unexpected quote in " + fileName + ".");
                    }

                    isQuoted = true;
                    hasPendingField = true;
                    continue;
                }

                field.Append(character);
                hasPendingField = true;
            }

            if (isQuoted)
            {
                throw new InvalidDataException("Unclosed quoted field in " + fileName + ".");
            }

            if (hasPendingField || record.Count > 0 || field.Length > 0)
            {
                record.Add(field.ToString());
                records.Add(record.ToArray());
            }

            return records;
        }

        private static List<TableColumn> GetTableColumns(
            System.Data.SQLite.SQLiteConnection connection,
            string tableName)
        {
            var columns = new List<TableColumn>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA table_info([" + tableName + "]);";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(new TableColumn
                        {
                            Name = reader.GetString(1),
                            Type = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            IsRequired = reader.GetInt32(3) != 0
                        });
                    }
                }
            }

            return columns;
        }

        private static void ValidateTable(
            CsvTable table,
            List<TableColumn> columns,
            string fileName)
        {
            if (table.Headers.Length != columns.Count ||
                table.Headers.Where((header, index) =>
                    !string.Equals(header, columns[index].Name, StringComparison.Ordinal)).Any())
            {
                throw new InvalidDataException(
                    fileName + " headers do not match the current database schema.");
            }

            for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                var row = table.Rows[rowIndex];
                if (row.Length != columns.Count)
                {
                    throw new InvalidDataException(
                        fileName + " row " + (rowIndex + 2) + " has an invalid number of columns.");
                }

                for (var columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                {
                    ValidateValue(row[columnIndex], columns[columnIndex], fileName, rowIndex + 2);
                }
            }

            ValidateUniqueIds(table, fileName);
        }

        private static void ValidateValue(
            string value,
            TableColumn column,
            string fileName,
            int rowNumber)
        {
            if (string.IsNullOrEmpty(value))
            {
                if (column.IsRequired && IsNumericColumn(column))
                {
                    throw new InvalidDataException(
                        fileName + " row " + rowNumber + ", column " + column.Name + " is required.");
                }

                return;
            }

            var type = column.Type.ToUpperInvariant();
            long integerValue;
            double realValue;

            if (type.Contains("INT") && !long.TryParse(
                    value,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out integerValue))
            {
                throw new InvalidDataException(
                    fileName + " row " + rowNumber + ", column " + column.Name + " must be an integer.");
            }

            if (IsNumericColumn(column) && !type.Contains("INT") &&
                !double.TryParse(
                    value,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out realValue))
            {
                throw new InvalidDataException(
                    fileName + " row " + rowNumber + ", column " + column.Name + " must be numeric.");
            }
        }

        private static void ValidateUniqueIds(CsvTable table, string fileName)
        {
            var idIndex = Array.IndexOf(table.Headers, "ID");
            var ids = new HashSet<long>();

            foreach (var row in table.Rows)
            {
                long id;
                if (idIndex < 0 || !long.TryParse(
                        row[idIndex],
                        NumberStyles.Integer,
                        CultureInfo.InvariantCulture,
                        out id) || !ids.Add(id))
                {
                    throw new InvalidDataException(fileName + " contains missing or duplicate IDs.");
                }
            }
        }

        private static void ValidateRelationships(
            CsvTable locations,
            CsvTable cards,
            CsvTable cardFaces)
        {
            var locationIds = GetIds(locations);
            var cardIds = GetIds(cards);

            ValidateForeignKeys(cards, "LocationID", locationIds, "Locations.csv");
            ValidateForeignKeys(cardFaces, "CardID", cardIds, "CardFaces.csv");
        }

        private static HashSet<long> GetIds(CsvTable table)
        {
            var idIndex = Array.IndexOf(table.Headers, "ID");
            return new HashSet<long>(table.Rows.Select(row => long.Parse(
                row[idIndex],
                CultureInfo.InvariantCulture)));
        }

        private static void ValidateForeignKeys(
            CsvTable table,
            string columnName,
            HashSet<long> parentIds,
            string fileName)
        {
            var columnIndex = Array.IndexOf(table.Headers, columnName);

            for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                var value = table.Rows[rowIndex][columnIndex];
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                long foreignKey;
                if (!long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out foreignKey) ||
                    !parentIds.Contains(foreignKey))
                {
                    throw new InvalidDataException(
                        fileName + " row " + (rowIndex + 2) + " references a missing " + columnName + ".");
                }
            }
        }

        private static void DeleteExistingData(
            System.Data.SQLite.SQLiteConnection connection,
            System.Data.SQLite.SQLiteTransaction transaction)
        {
            foreach (var tableName in new[] { "CardFace", "Card", "Location" })
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = "DELETE FROM [" + tableName + "];";
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void InsertTable(
            System.Data.SQLite.SQLiteConnection connection,
            System.Data.SQLite.SQLiteTransaction transaction,
            string tableName,
            List<TableColumn> columns,
            CsvTable table)
        {
            var columnNames = string.Join(", ", columns.Select(column => "[" + column.Name + "]"));
            var parameterNames = columns.Select((column, index) => "@value" + index).ToArray();

            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = "INSERT INTO [" + tableName + "] (" + columnNames + ") VALUES (" +
                                      string.Join(", ", parameterNames) + ");";

                foreach (var parameterName in parameterNames)
                {
                    command.Parameters.Add(new System.Data.SQLite.SQLiteParameter(parameterName));
                }

                foreach (var row in table.Rows)
                {
                    for (var index = 0; index < row.Length; index++)
                    {
                        command.Parameters[index].Value = ShouldUseDatabaseNull(row[index], columns[index])
                            ? (object)DBNull.Value
                            : row[index];
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        private static bool ShouldUseDatabaseNull(string value, TableColumn column)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return false;
            }

            return IsNumericColumn(column);
        }

        private static bool IsNumericColumn(TableColumn column)
        {
            var type = column.Type.ToUpperInvariant();
            return type.Contains("INT") || type.Contains("REAL") || type.Contains("FLOA") ||
                   type.Contains("DOUB") || type.Contains("DEC") || type.Contains("NUM");
        }

        private static void ExportTable(
            System.Data.SQLite.SQLiteConnection connection,
            System.Data.SQLite.SQLiteTransaction transaction,
            ZipArchive archive,
            string tableName,
            string fileName)
        {
            var entry = archive.CreateEntry(fileName, CompressionLevel.Optimal);

            using (var entryStream = entry.Open())
            using (var writer = new StreamWriter(entryStream, new UTF8Encoding(true)))
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = "SELECT * FROM [" + tableName + "] ORDER BY ID;";

                using (var reader = command.ExecuteReader())
                {
                    for (var column = 0; column < reader.FieldCount; column++)
                    {
                        if (column > 0)
                        {
                            writer.Write(',');
                        }

                        writer.Write(EscapeCsvValue(reader.GetName(column)));
                    }

                    writer.WriteLine();

                    while (reader.Read())
                    {
                        for (var column = 0; column < reader.FieldCount; column++)
                        {
                            if (column > 0)
                            {
                                writer.Write(',');
                            }

                            var value = reader.IsDBNull(column)
                                ? string.Empty
                                : ConvertToCsvText(reader.GetValue(column));
                            writer.Write(EscapeCsvValue(value));
                        }

                        writer.WriteLine();
                    }
                }
            }
        }

        private static string ConvertToCsvText(object value)
        {
            var formattable = value as IFormattable;
            return formattable == null
                ? value.ToString()
                : formattable.ToString(null, CultureInfo.InvariantCulture);
        }

        private static string EscapeCsvValue(string value)
        {
            value = value ?? string.Empty;

            if (value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) < 0)
            {
                return value;
            }

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
