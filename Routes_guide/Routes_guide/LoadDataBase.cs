using MySql.Data.MySqlClient;
using System;
namespace Routes_guide
{
    class LoadDataBase
    {
        private static string connectStr = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";

        public bool connectDataBase()
        {
            //Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = GetDBConnection();
 
            try
            {
                conn.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
            return true;
        }

        public static MySqlConnection GetDBConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectStr);
            return conn;
        }
    }
}