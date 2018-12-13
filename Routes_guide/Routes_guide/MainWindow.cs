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
        struct StopPoints
        {
            public int idRoute;
            public int idPoint;
        }

        private Start_Window start;
        private About about;
        private string name = "name";

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
            
            List<StopPoints> array1 = new List<StopPoints>();
            List<StopPoints> array2 = new List<StopPoints>();
            List<int> result = new List<int>();
            

            MySqlConnection conn1 = new MySqlConnection(conneStr);
            MySqlConnection conn2 = new MySqlConnection(connectStr);
            MySqlConnection conn3 = new MySqlConnection(connectStr2);

            conn1.Open();
            conn2.Open();
            conn3.Open();
            string request1;
            string request2;

            request1 = "SELECT idPoint, id FROM " + points + " WHERE namePoint='" + A + "';";
            request2 = "SELECT idPoint, id FROM " + points + " WHERE namePoint='" + B + "';";

            MySqlCommand cmdBus1 = new MySqlCommand(request1, conn1);
            MySqlCommand cmdBus2 = new MySqlCommand(request2, conn2);

            MySqlDataReader reader1 = cmdBus1.ExecuteReader();
            MySqlDataReader reader2 = cmdBus2.ExecuteReader();

            while (true)
            {
                StopPoints sp;
                if (reader1.Read() == true)
                {
                    sp.idRoute = (int)reader1[0];
                    sp.idPoint = (int)reader1[1];
                    array1.Add(sp);

                }
                else
                {
                    if (reader2.Read() == true)
                    {
                        sp.idRoute = (int)reader2[0];
                        sp.idPoint = (int)reader2[1];
                        array2.Add(sp);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            foreach (StopPoints x in array1)
            {
                foreach (StopPoints y in array2)
                {
                    if(x.idRoute == y.idRoute && x.idPoint < y.idPoint)
                    {
                        result.Add(y.idRoute);
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
            table.Columns.Add("Время в пути");
            table.Columns.Add("Место отправления");
            table.Columns.Add("Место прибытия");
            string requestResult;

            foreach (int req in result)
            {
                if (type == "Все типы" || type == "Тип транспорта")
                {
                    requestResult = "SELECT route, type, departTime, arrivalTime, timeForTravell FROM " + tab_transports + " WHERE id=" + req + ";";
                }
                else
                {
                    requestResult = "SELECT route, type, departTime, arrivalTime, timeForTravell FROM " + tab_transports + " WHERE id=" + req + " AND type='" + type + "';";
                }

                MySqlCommand command = new MySqlCommand(requestResult, conn3);
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    table.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[3].ToString(), read[4].ToString(), this.textBox1.Text, this.textBox2.Text);
                }
                read.Close();
            }
            dataGridView1.DataSource = table;
            conn1.Close();
            conn2.Close();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string A = this.textBox1.Text;
            string B = this.textBox2.Text;
            string type_trans = this.typeComboBox.Text;
            search(A, B, type_trans);
        }

        public void setName(string n)
        {
            this.name = n;
        }

        private void MainWindow_VisibleChanged(object sender, EventArgs e)
        {
            this.label4.Text = name;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about = new About();
            about.Visible = true;
        }

        private void showAll(string type)
        {
            string conneStr = "server=localhost;user=root;database=transport;password=2211lalka_Kobra;";
            string tab_transports = "routes";
            MySqlConnection c1 = new MySqlConnection(conneStr);

            c1.Open();
            string request;
            if (type == "Все типы")
            {
                request = "SELECT * FROM " + tab_transports + ";";
            }
            else
            {
                request = "SELECT * FROM " + tab_transports + " WHERE type='" + type + "';";
            }
           
            MySqlCommand cmd = new MySqlCommand(request, c1);
            MySqlDataReader reader = cmd.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("№");
            table.Columns.Add("Маршрут");
            table.Columns.Add("Тип");
            table.Columns.Add("Время отправления");
            table.Columns.Add("Время прибытия");
            table.Columns.Add("Время в пути");

            

            while (reader.Read())
            {
                table.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[5].ToString(), reader[3].ToString(), reader[4].ToString(), reader[2].ToString());
            }
            dataGridView2.DataSource = table;
            c1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string type_trans = this.typeComboBox2.Text;
            if (type_trans == "Тип транспорта")
            {

            }
            else showAll(type_trans);
        }
    }
}
