using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SQL_trans
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conneStr = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";
            string connectStr = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";
            string connectStr2 = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";

            string tab_transports = "routes";
            string points = "stopPoints";
            string A = "Пуховичи";
            string B = "Бобруйск";
            List<int> array1 = new List<int>();
            List<int> array2 = new List<int>();
            List<int> result = new List<int>();

            MySqlConnection conn1 = new MySqlConnection(conneStr);
            MySqlConnection conn2 = new MySqlConnection(connectStr);
            MySqlConnection conn3 = new MySqlConnection(connectStr2);

            conn1.Open();
            conn2.Open();
            conn3.Open();
            //string bus = "SELECT route, 'Время отправления' FROM bus WHERE id = 1";
            //string sql = "SELECT idnew_table, name FROM men WHERE age = 45";

            string request1 = "SELECT idPoint FROM " + points + " WHERE namePoint='" + A + "';";
            string request2 = "SELECT idPoint FROM " + points + " WHERE namePoint='" + B + "';";
            MySqlCommand cmdBus1 = new MySqlCommand(request1, conn1);
            MySqlCommand cmdBus2 = new MySqlCommand(request2, conn2);

            //MySqlCommand command = new MySqlCommand(sql, conn1);
            MySqlDataReader reader1 = cmdBus1.ExecuteReader();
            MySqlDataReader reader2 = cmdBus2.ExecuteReader();

            while (true)
            {
                if (reader1.Read() == true)
                {
                    array1.Add((int)reader1[0]);
                }
                else
                {
                    if (reader2.Read() == true)
                    {
                        array2.Add((int)reader2[0]);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            foreach (int x in array1)
            {
                foreach (int y in array2)
                {
                    if (x == y)
                    {
                        result.Add(y);
                    }
                }
            }

            reader1.Close();
            reader2.Close();

            DataTable table = new DataTable();
            table.Columns.Add("Маршрут");
            table.Columns.Add("Время отправления");
            table.Columns.Add("Время прибытия");

            foreach (int req in result)
            {
                string requestResult = "SELECT route, departTime, arrivalTime FROM " + tab_transports + " WHERE id=" + req + ";";
                MySqlCommand command = new MySqlCommand(requestResult, conn3);
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    table.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString());
                }
                read.Close();
            }
            dataGridView1.DataSource = table;
            //string requestResult = "SELECT * FROM " + tab_transports + " WHERE id=" + B + "';";
            //MySqlCommand command = new MySqlCommand(sql, conn);
            //string name = command.ExecuteScalar().ToString();// возвратит лишь первую строку с строкой где имя поле jack
            //Console.WriteLine(name);


            conn1.Close();
            //conn2.Close();



        }
    }
}
