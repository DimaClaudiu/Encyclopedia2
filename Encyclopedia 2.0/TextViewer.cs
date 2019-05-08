using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Encyclopedia_2._0
{
    public partial class TextViewer : MetroFramework.Forms.MetroForm
    {
        bool saved = false;
        int index = 0;
        string cache;
        List<Entry> myEntries;

        bool checkModified()
        {
            return (richTextBox1.Text != cache);
        }

        void Edit()
        {
            //myEntries[index].Text = richTextBox1.Rtf;

            string connectionString = GlobalConstants.DatabaseConnectionPath;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Entry SET Text = '" + richTextBox1.Rtf + "' Where Id = " + (myEntries[index].Index.ToString());
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            foreach (Form Faux in Application.OpenForms)
            {
                if (Faux.Name == "Form1")
                {
                    (Faux as Form1).loadEntries();
                }
            }

        }

        public TextViewer(List<Entry> entri, int index)
        {
            InitializeComponent();

            this.index = index;
            myEntries = entri;
           
            richTextBox1.Rtf = entri[index].Text;
            cache = richTextBox1.Text;
            
        }

        private void TextViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkModified() == true)
            {
                if (saved == false)
                {
                    DialogResult dialogResult = MessageBox.Show("Save entry?", "Don't forget me :(", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        saved = true;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }

                if (saved == true)
                {
                    Edit();
                }

            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Edit();
                cache = richTextBox1.Text;
                saved = true;
            }

            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else
            {
                saved = false;
            }
        }
    }
}
