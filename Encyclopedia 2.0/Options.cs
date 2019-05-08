using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace Encyclopedia_2._0
{
    public partial class Options : MetroFramework.Forms.MetroForm
    {
        Timer myTimer1, myTimer2;
        public Options(Timer timer1,Timer timer2)
        {
            InitializeComponent();
            myTimer1 = timer1;
            myTimer2 = timer2;
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This requires the removal of all entries.", "Are you sure?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("All your entries WILL BE GONE, proceed?", "Are you really sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result2 == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(GlobalConstants.DatabaseConnectionPath))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "dbcc checkident ([Entry],reseed,0);";
                        connection.Open();
                        command.ExecuteNonQuery();
                        command.CommandText = "truncate table Entry";
                        command.ExecuteNonQuery();
                        connection.Close();

                        foreach (Form fAux in Application.OpenForms)
                        {
                            if (fAux.Name == "Form1")
                                (fAux as Form1).loadEntries();
                        }
                    }
                }
            }
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will remove all your entires.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("All your entries WILL BE GONE, proceed?", "Are you really sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result2 == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(GlobalConstants.DatabaseConnectionPath))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "truncate table Entry";
                        command.ExecuteNonQuery();
                        connection.Close();

                        foreach (Form fAux in Application.OpenForms)
                        {
                            if (fAux.Name == "Form1")
                                (fAux as Form1).loadEntries();
                        }
                    }
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            myTimer1.Enabled = !myTimer1.Enabled;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
               RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    rk.SetValue("Encyclopedia", Application.ExecutablePath.ToString());
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    rk.DeleteValue("Encyclopedia", false);
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            GlobalConstants.MinimizeToTray = !GlobalConstants.MinimizeToTray;
            if(GlobalConstants.MinimizeToTray)
            {
                MessageBox.Show("Minimize to tray enabled!");
            }
            else
            {
                MessageBox.Show("Minimize to tray disabled!");
            }
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            myTimer2.Enabled = !myTimer2.Enabled;
        }
    }
}
