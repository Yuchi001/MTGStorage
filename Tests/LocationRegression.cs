using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using MTGStorage;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;
using MTGStorage.Features;
using MTGStorage.CustomUI;
class LocationRegression
{
    static void Assert(bool ok,string message) { if(!ok) throw new Exception(message); }
    static long Scalar(SQLiteConnection c,string sql) { using(var cmd=c.CreateCommand()){cmd.CommandText=sql;return Convert.ToInt64(cmd.ExecuteScalar());} }
    static void Sql(SQLiteConnection c,string sql) { using(var cmd=c.CreateCommand()){cmd.CommandText=sql;cmd.ExecuteNonQuery();} }
    static void Move(SQLiteConnection c,int from,int to,Dictionary<int,int> items,bool all=false)
    {
        try { typeof(RelocationEndpoints).GetMethod("Relocate",BindingFlags.Static|BindingFlags.NonPublic).Invoke(null,new object[]{c,from,to,items,all}); }
        catch(TargetInvocationException e) { throw e.InnerException; }
    }
    static void Reject(Action action) { try {action();} catch(InvalidOperationException){return;} catch(SQLiteException){return;} throw new Exception("Expected rejection"); }
    [STAThread] static void Main()
    {
        using(var c=new SQLiteConnection("Data Source=:memory:;Foreign Keys=True;"))
        {
            c.Open();
            Sql(c,"CREATE TABLE Location(ID INTEGER PRIMARY KEY,Capacity INTEGER,Code TEXT DEFAULT 'Box',MinPrice NUMERIC DEFAULT 0); CREATE TABLE Card(ID INTEGER PRIMARY KEY,LocationID INTEGER REFERENCES Location(ID),Count INTEGER,Name TEXT,PrintID TEXT DEFAULT 'Print',Price NUMERIC DEFAULT 5); CREATE TABLE CardFace(ID INTEGER PRIMARY KEY,CardID INTEGER REFERENCES Card(ID),Name TEXT); INSERT INTO Location(ID,Capacity) VALUES(1,100),(2,100),(3,1); INSERT INTO Card(ID,LocationID,Count,Name) VALUES(10,1,8,'Double face'),(20,1,2,'Other'); INSERT INTO CardFace VALUES(1,10,'Front'),(2,10,'Back');");
            Move(c,1,2,new Dictionary<int,int>{{10,3}});
            Assert(Scalar(c,"SELECT SUM(Count) FROM Card")==10,"Conserve total");
            Assert(Scalar(c,"SELECT Count FROM Card WHERE ID=10")==5,"Partial source");
            Assert(Scalar(c,"SELECT COUNT(*) FROM CardFace f JOIN Card c ON c.ID=f.CardID WHERE c.LocationID=2")==2,"Copy both faces");
            Reject(()=>Move(c,1,3,new Dictionary<int,int>{{10,2}}));
            Reject(()=>Move(c,1,2,new Dictionary<int,int>{{10,6}}));
            Reject(()=>Move(c,1,1,new Dictionary<int,int>{{10,1}}));
            Reject(()=>Move(c,1,999,new Dictionary<int,int>{{10,1}}));
            Reject(()=>Move(c,2,1,new Dictionary<int,int>{{10,1}}));
            Sql(c,"CREATE TRIGGER FailMove BEFORE UPDATE ON Card WHEN OLD.ID=20 BEGIN SELECT RAISE(ABORT,'test failure'); END;");
            Reject(()=>Move(c,1,2,new Dictionary<int,int>{{10,5},{20,2}}));
            Assert(Scalar(c,"SELECT LocationID FROM Card WHERE ID=10")==1,"Atomic rollback");
            Sql(c,"DROP TRIGGER FailMove;");
            Move(c,1,2,new Dictionary<int,int>(),true);
            Assert(Scalar(c,"SELECT COUNT(*) FROM Card WHERE LocationID=1")==0,"All cards moved");
            Assert(Scalar(c,"SELECT COUNT(*) FROM CardFace WHERE CardID=10")==2,"Whole row keeps faces");
            Assert(Scalar(c,"SELECT SUM(Count) FROM Card")==10,"All preserves total");
            Sql(c,"INSERT INTO Location(ID,Capacity,MinPrice) VALUES(4,100,3),(5,100,10); INSERT INTO Card(ID,LocationID,Count,Name,Price) VALUES(30,1,4,'Tier card',5);");
            Reject(()=>Move(c,1,2,new Dictionary<int,int>{{30,1}}));
            Reject(()=>Move(c,1,5,new Dictionary<int,int>{{30,1}}));
            Move(c,1,0,new Dictionary<int,int>{{30,4}});
            Assert(Scalar(c,"SELECT LocationID FROM Card WHERE ID=30")==4,"Automatic highest matching minimum");
            Sql(c,"UPDATE Location SET Capacity=5 WHERE ID=4; INSERT INTO Location(ID,Capacity,MinPrice) VALUES(6,20,3); INSERT INTO Card(ID,LocationID,Count,Name,Price) VALUES(40,1,4,'Tier card',5);");
            Move(c,1,0,new Dictionary<int,int>{{40,4}});
            Assert(Scalar(c,"SELECT SUM(Count) FROM Card WHERE LocationID=4")==5,"Automatic fills matching existing card first");
            Assert(Scalar(c,"SELECT SUM(Count) FROM Card WHERE LocationID=6")==3,"Automatic splits remainder");
            Sql(c,"INSERT INTO Card(ID,LocationID,Count,Name,Price) VALUES(50,1,1,'Cheap',0.1),(60,1,1,'Expensive',5);");
            Move(c,1,0,new Dictionary<int,int>{{50,1},{60,1}});
            Assert(Scalar(c,"SELECT LocationID FROM Card WHERE ID=50")!=6,"Mixed price bands separate cheap card");
            Assert(Scalar(c,"SELECT LocationID FROM Card WHERE ID=60")==6,"Mixed price bands route expensive card");
            Sql(c,"UPDATE Location SET Capacity=12 WHERE ID=6; INSERT INTO Card(ID,LocationID,Count,Name,Price) VALUES(70,6,5,'Filler',5),(80,1,1,'Unmatched',5);");
            Reject(()=>Move(c,1,0,new Dictionary<int,int>{{80,1}}));
            Assert(Scalar(c,"SELECT LocationID FROM Card WHERE ID=80")==1,"75 percent rule preserves source");
            Sql(c,"INSERT INTO Card(ID,LocationID,Count,Name,Price) VALUES(90,1,1000,'Too many',5);");
            Reject(()=>Move(c,1,0,new Dictionary<int,int>{{80,1},{90,1000}}));
            Assert(Scalar(c,"SELECT Count FROM Card WHERE ID=90")==1000,"Failed auto plan leaves all stock unchanged");
            Console.WriteLine("PASS: rejects wrong price tiers, automatic matching, splitting and mixed prices");
            Console.WriteLine("PASS: partial/full/all relocation, faces, capacity/stale/source checks, transaction rollback");
        }
        using(var form=new LocationCardsWindow())
        using(var original=new CardSearchWindow())
        {
            Assert(form.ClientSize==original.ClientSize,"Same proportions");
            var manager=new ShipmentManager();
            var card=new Card {Count=150,ImageUrl=""};
            manager.SetCard(card,120);
            using(var element=new CardElement(card,manager))
            {
                var input=(NumericUpDown)typeof(CardElement).GetField("cardToShipTextbox",BindingFlags.Instance|BindingFlags.NonPublic).GetValue(element);
                Assert(input.Value==120 && input.Maximum==150,"Restore selection beyond 100");
            }
            Assert(manager.GetSelectedCards().Single().Count==120,"Selection preserved");
            var flags=BindingFlags.Instance|BindingFlags.NonPublic;
            var fixture=Enumerable.Range(1,9).Select(i=>new Card {ID=i,Count=12,ImageUrl="",Price=1.25m}).ToList();
            typeof(LocationCardsWindow).GetField("cards",flags).SetValue(form,fixture);
            var page=(ToolStripTextBox)typeof(LocationCardsWindow).GetField("page",flags).GetValue(form);
            typeof(LocationCardsWindow).GetMethod("LoadPage",flags).Invoke(form,null);
            var panel=(FlowLayoutPanel)typeof(LocationCardsWindow).GetField("cardPanel",flags).GetValue(form);
            Assert(panel.Controls.Count==8,"Eight cards per page");
            var selection=(NumericUpDown)typeof(CardElement).GetField("cardToShipTextbox",flags).GetValue(panel.Controls[0]);
            selection.Value=3;
            typeof(LocationCardsWindow).GetMethod("GoToPage",flags).Invoke(form,new object[]{2});
            Assert(panel.Controls.Count==1,"Last page");
            typeof(LocationCardsWindow).GetMethod("GoToPage",flags).Invoke(form,new object[]{1});
            selection=(NumericUpDown)typeof(CardElement).GetField("cardToShipTextbox",flags).GetValue(panel.Controls[0]);
            Assert(selection.Value==3,"Selection survives pagination");
            page.Text="invalid";
            typeof(LocationCardsWindow).GetMethod("CommitPage",flags).Invoke(form,null);
            Assert(page.Text=="1","Invalid page input is safe");
            ((ToolStripButton)typeof(LocationCardsWindow).GetField("last",flags).GetValue(form)).PerformClick();
            Assert(panel.Controls.Count==1,"Navigator last button");
            ((ToolStripButton)typeof(LocationCardsWindow).GetField("first",flags).GetValue(form)).PerformClick();
            Assert(panel.Controls.Count==8,"Navigator first button");
            panel.PerformLayout();
            Assert(panel.Controls.Cast<Control>().All(c=>c.Right<=panel.ClientSize.Width && c.Bottom<=panel.ClientSize.Height),"All eight cards fit inside the panel");
            Console.WriteLine("PASS: window proportions and persistent quantities");
        }
    }
}
