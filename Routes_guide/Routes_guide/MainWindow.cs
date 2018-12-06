﻿using System;
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
    public partial class MainWindow : Form
    {
        private Start_Window start;

        public MainWindow(Start_Window s)
        {
            start = s;
            InitializeComponent();
        }

        private void MainWindow_FormClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
