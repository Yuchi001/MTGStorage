using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace MTGStorage.Database
{
    public static class Database
    {
        private static readonly string DbPath =
            Path.Combine(Application.StartupPath, "mtgstorage.db");

        private static readonly string ConnectionString =
            "Data Source=" + DbPath + ";Version=3;Foreign Keys=True;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public static void Initialize()
        {
            if (!File.Exists(DbPath))
            {
                SQLiteConnection.CreateFile(DbPath);
            }

            using (var connection = GetConnection())
            {
                connection.Open();
                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Location (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Code TEXT NOT NULL,
                        Capacity INTEGER NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Card (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        PrintID TEXT NOT NULL,
                        ImageUrl TEXT NOT NULL,
                        Count INTEGER,
                        LocationID INTEGER,
                        FOREIGN KEY (LocationID) REFERENCES Location(ID) ON DELETE CASCADE
                    );

                    CREATE TRIGGER IF NOT EXISTS DeleteCardsAfterLocationDelete
                    AFTER DELETE ON Location
                    BEGIN
                        DELETE FROM Card WHERE LocationID = OLD.ID;
                    END;

                    CREATE TABLE IF NOT EXISTS CardFace (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CardID INTEGER NOT NULL,
                        Name TEXT NOT NULL,
                        OracleText TEXT NOT NULL,
                        ImageUrl TEXT NOT NULL,
                        Colors TEXT NOT NULL,
                        ManaCost TEXT NOT NULL,
                        Types TEXT NOT NULL,
                        FOREIGN KEY (CardID) REFERENCES Card(ID) ON DELETE CASCADE
                    );
                ";

                    command.ExecuteNonQuery();
                }

                EnsureCardFaceCascadeDelete(connection);
                EnsureCardFaceColumns(connection);
            }
        }

        private static void EnsureCardFaceCascadeDelete(SQLiteConnection connection)
        {
            var hasCascadeDelete = false;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA foreign_key_list(CardFace);";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[2].ToString() == "Card" &&
                            reader[3].ToString() == "CardID" &&
                            reader[6].ToString() == "CASCADE")
                        {
                            hasCascadeDelete = true;
                            break;
                        }
                    }
                }
            }

            if (hasCascadeDelete)
            {
                return;
            }

            var colorsColumn = HasCardFaceColumn(connection, "Colors")
                ? "Colors"
                : "''";
            var manaCostColumn = HasCardFaceColumn(connection, "ManaCost")
                ? "ManaCost"
                : "''";

            using (var transaction = connection.BeginTransaction())
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = $@"
                    ALTER TABLE CardFace RENAME TO CardFace_Old;

                    CREATE TABLE CardFace (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CardID INTEGER NOT NULL,
                        Name TEXT NOT NULL,
                        OracleText TEXT NOT NULL,
                        ImageUrl TEXT NOT NULL,
                        Colors TEXT NOT NULL,
                        ManaCost TEXT NOT NULL,
                        Types TEXT NOT NULL,
                        FOREIGN KEY (CardID) REFERENCES Card(ID) ON DELETE CASCADE
                    );

                    INSERT INTO CardFace
                        (ID, CardID, Name, OracleText, ImageUrl, Colors, ManaCost, Types)
                    SELECT
                        ID, CardID, Name, OracleText, ImageUrl,
                        {colorsColumn}, {manaCostColumn}, Types
                    FROM CardFace_Old;

                    DROP TABLE CardFace_Old;";

                command.ExecuteNonQuery();
                transaction.Commit();
            }
        }

        private static void EnsureCardFaceColumns(SQLiteConnection connection)
        {
            if (!HasCardFaceColumn(connection, "Colors"))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "ALTER TABLE CardFace ADD COLUMN Colors TEXT NOT NULL DEFAULT '';";
                    command.ExecuteNonQuery();
                }
            }

            if (!HasCardFaceColumn(connection, "ManaCost"))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "ALTER TABLE CardFace ADD COLUMN ManaCost TEXT NOT NULL DEFAULT '';";
                    command.ExecuteNonQuery();
                }
            }
        }

        private static bool HasCardFaceColumn(
            SQLiteConnection connection,
            string columnName)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA table_info(CardFace);";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[1].ToString() == columnName)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
