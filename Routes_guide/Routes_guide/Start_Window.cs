using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Routes_guide
{
    public partial class Start_Window : Form
    {
        private LoadDataBase loadDataBase;
        private MainWindow mainWindow;
        
        public Start_Window()
        {
            InitializeComponent();
            mainWindow = new MainWindow(this) { Visible = false };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadDataBase = new LoadDataBase();
            if (loadDataBase.connectDataBase())
            {
                this.Visible = false;
                mainWindow.Visible = true;
            }
            else
            {
                label1.Text = "НЕТ СОЕДИНЕНИЯ С MySQL";
            }
        }
    }
}
