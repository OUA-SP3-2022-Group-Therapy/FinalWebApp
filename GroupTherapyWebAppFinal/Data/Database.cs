using System.Data.SQLite;
using System.IO;

namespace GroupTherapyWebAppFinal.Data
{
    public class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=Database.db");
            if (!File.Exists("./Database.db"))
            {
                SQLiteConnection.CreateFile("Database.db");
            }
            
        }

    }
}
