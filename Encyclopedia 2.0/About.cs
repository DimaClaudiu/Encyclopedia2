using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encyclopedia_2._0
{
    public partial class About : MetroFramework.Forms.MetroForm
    {
        private void initializeLabels()
        {
            label1.Text = "Version: " + GlobalConstants.Version;
            label2.Text = "Developed by: " + "Dima Claudiu Alexandru";
            label3.Text = "Class: " + "12 A";
            label4.Text = "Year: " + "2016 - 2017";
            label5.Text = "Teacher: " + "Aldea Cristina"; 
        }
        public About()
        {
            InitializeComponent();
            initializeLabels();
        }
    }
}
