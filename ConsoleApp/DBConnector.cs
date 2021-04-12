using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ConsoleApp
{
    class DBConnector
    {
        public static void Test()
        {

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (OleDbConnection connection = new OleDbConnection(ConsoleApp.Properties.Settings.Default.ConnectionString))
            {
                OleDbCommand command = new OleDbCommand("Select * From `Область изображение`");

                // Set the Connection to the new OleDbConnection.
                command.Connection = connection;

                // Open the connection and execute the insert command.
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var a = reader[2];
                        Console.WriteLine($"{reader[1]} {reader[2]} {reader[3]} {reader[4]} {reader[5]}");
                    }
                    reader.Close();

                    //foreach (var item in response)
                    //    Console.WriteLine(((System.Data.Common.DataRecordInternal)item)[1]);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // The connection is automatically closed when the
                // code exits the using block.

            }
        }
    }
}
