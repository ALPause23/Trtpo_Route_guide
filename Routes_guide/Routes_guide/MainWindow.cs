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

namespace Routes_guide
{
    public partial class MainWindow : Form
    {
        private Start_Window start;
        //private SearchRoute searRoute;

        public MainWindow(Start_Window s)
        {
            start = s;
            InitializeComponent();
        }

        private void MainWindow_FormClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void search(string A, string B, string type)
        {
            string conneStr = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";
            string connectStr = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";
            string connectStr2 = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";

            string tab_transports = "routes";
            string points = "stopPoints";
            
            List<int> array1 = new List<int>();
            List<int> array2 = new List<int>();
            List<int> result = new List<int>();

            MySqlConnection conn1 = new MySqlConnection(conneStr);
            MySqlConnection conn2 = new MySqlConnection(connectStr);
            MySqlConnection conn3 = new MySqlConnection(connectStr2);

            conn1.Open();
            conn2.Open();
            conn3.Open();
            string request1;
            string request2;

            request1 = "SELECT idPoint FROM " + points + " WHERE namePoint='" + A + "';";
            request2 = "SELECT idPoint FROM " + points + " WHERE namePoint='" + B + "';";

            MySqlCommand cmdBus1 = new MySqlCommand(request1, conn1);
            MySqlCommand cmdBus2 = new MySqlCommand(request2, conn2);

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
            table.Columns.Add("Тип");
            table.Columns.Add("Время отправления");
            table.Columns.Add("Время прибытия");
            string requestResult;

            foreach (int req in result)
            {
                if (type == "Все типы")
                {
                    requestResult = "SELECT route, type, departTime, arrivalTime FROM " + tab_transports + " WHERE id=" + req + ";";
                }
                else
                {
                    requestResult = "SELECT route, type, departTime, arrivalTime FROM " + tab_transports + " WHERE id=" + req + " AND type='" + type + "';";
                }

                MySqlCommand command = new MySqlCommand(requestResult, conn3);
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    table.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[3].ToString());
                }
                read.Close();
            }
            dataGridView1.DataSource = table;
            conn1.Close();
            conn2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string A = this.textBox1.Text;
            string B = this.textBox2.Text;
            string type_trans = this.typeComboBox.Text;

            search(A, B, type_trans);

        }
    }
}
