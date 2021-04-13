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


        public static Param GetParam(string Name)
        {
            using (OleDbConnection connection = new OleDbConnection(conn_str))
            {
                DataContext db = new DataContext(connection);
                Table<Param> params_ = db.GetTable<Param>();
                return params_.ToList().First(par => par.Name == Name);
            }
        }

        public static List<ImageArea> GetImageAreas(string fileName)
        {
            using (OleDbConnection connection = new OleDbConnection(conn_str))
            {
                DataContext db = new DataContext(connection);
                Table<ImageArea> params_ = db.GetTable<ImageArea>();
                return params_.Where(area => area.FileName == fileName).ToList();
            }
        }

        public static List<ImageFile> GetImageFiles()
        {
            using (OleDbConnection connection = new OleDbConnection(conn_str))
            {
                DataContext db = new DataContext(connection);
                Table<ImageFile> files = db.GetTable<ImageFile>();
                return files.ToList();
            }
        }
    }
}
