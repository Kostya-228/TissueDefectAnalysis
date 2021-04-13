using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Linq;

namespace ConsoleApp
{
    class DBConnector
    {

        private static string conn_str = ConsoleApp.Properties.Settings.Default.ConnectionString;


        public static void GetParam(string Name)
        {
            using (OleDbConnection connection = new OleDbConnection(conn_str))
            {
                DataContext db = new DataContext(connection);
                Table<Param> params_ = db.GetTable<Param>();
                Param a = params_.ToList().First(par => par.Name == "B");
            }
        }

        public static void Test()
        {
            OleDbConnection connection = new OleDbConnection(conn_str);
            DataContext db = new DataContext(connection);
            Table<Param> params_ = db.GetTable<Param>();
            Param a = params_.ToList().First(par => par.Name == "B");

            Console.WriteLine($"{a.Name} {a.Min} {a.Max} {a.Step}");

            //foreach (var user in params_)
            //{
            //    Console.WriteLine("{0} \t{1} \t{2}", user.Min, user.Max, user.Step);
            //}
        }

        public static DataTable GetImageAreas()
        {
            var ds = RunSelect("Select * From `Область изображение`");
            return ds.Tables[0];
        }

        public static DataSet GetParams()
        {
            return RunSelect("Select * From `Параметры`");
            //return ds.Tables[0];
        }

        public static DataSet RunSelect(string cmd)
        {
            using (OleDbConnection connection = new OleDbConnection(ConsoleApp.Properties.Settings.Default.ConnectionString))
            {
                OleDbCommand command = new OleDbCommand(cmd);
                command.Connection = connection;
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }
    }
}
