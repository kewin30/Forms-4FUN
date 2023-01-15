using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp4
{
    internal class Database
    {
        public SQLiteConnection myconn;
        //string myconn;
        public Database()
        {
            myconn = new SQLiteConnection("Data Source=database.sqlite3");

            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
            }

        }
        public void OpenConnection()
        {
            if (myconn.State != System.Data.ConnectionState.Open)
            {
                myconn.Open();
            }
        }
        public void CloseConnection()
        {
            if (myconn.State != System.Data.ConnectionState.Closed)
            {
                myconn.Clone();
            }
        }
    }
}
