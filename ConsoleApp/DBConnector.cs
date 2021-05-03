﻿using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Linq;
using ConsoleApp.Models;

namespace ConsoleApp
{
    public class DBConnector
    {

        public static string conn_str = ConsoleApp.Properties.Settings.Default.ConnectionString;


        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(conn_str);
        }

        public static Param GetParam(string Name)
        {
            using (OleDbConnection connection = GetConnection())
            {
                DataContext db = new DataContext(connection);
                Table<Param> params_ = db.GetTable<Param>();
                return params_.ToList().First(par => par.Name == Name);
            }
        }

        public static List<T> GetList<T>() where T : class
        {
            using (OleDbConnection connection = GetConnection())
            {
                DataContext db = new DataContext(connection);
                Table<T> table = db.GetTable<T>();
                return table.ToList();
            }
        }

        public static void CreateList<T>(IEnumerable<T> items) where T : class
        {
            using (OleDbConnection connection = GetConnection())
            {
                DataContext db = new DataContext(connection);
                foreach (var item in items)
                {
                    db.GetTable<T>().InsertOnSubmit(item);
                }
                db.SubmitChanges();
            }
        }

        public static void CreateItem<T>(T item) where T : class
        {
            using (OleDbConnection connection = GetConnection())
            {
                DataContext db = new DataContext(connection);
                db.GetTable<T>().InsertOnSubmit(item);
                db.SubmitChanges();
            }
        }

        public static void UpdateList<T>(IEnumerable<T> items) where T : AccessModelProxy
        {
            using (OleDbConnection connection = GetConnection())
            {
                connection.Open();
                foreach (AccessModelProxy item in items)
                {
                    item.Update(connection);
                }
            }
        }
    }
}
