using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Encyclopedia_2._0
{
    public partial class TextEditor : MetroFramework.Forms.MetroForm
    {
        bool saved = false;
        string cache = "";
        string cacheTitle;
        List<Entry> entries = null;
        private ListView mainView = null;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QuickEntries.mdf;Integrated Security=True";

        bool checkModified()
        {
            return (richTextBox1.Text != cache || metroTextBox1.Text != cacheTitle);
        }

        void checkKeyShortcut()
        {
            
        }

        void Write()
        {
            //string separator = " <end>";
            ////Writing to file
            //StreamWriter s = File.AppendText(@"D:\Projects\My Life's Encyclopedia\Quick Entries\Quick Entries.rtf");


            //s.WriteLine(DateTime.Now.ToString("dd/MM/yy")); //Date
            //s.WriteLine(metroTextBox1.Text); //Title
            //s.WriteLine(richTextBox1.Rtf + separator); //Actual content in RTF format + a separator to know when to stop reading for the text
            //s.WriteLine(); //Empty line, for legibility
            //s.Close();

            //Updateing database
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                
                command.CommandText =  "INSERT INTO Entry values('" + DateTime.Now + "','" + metroTextBox1.Text + "','" + richTextBox1.Rtf + "')";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
           
            //adding entry to list
            Entry entry = new Entry(DateTime.Now.ToString("dd/MM/yy"), metroTextBox1.Text,richTextBox1.Rtf,entries.Count + 1);
            entries.Add(entry);

            //add item to ListView
            ListViewItem lvi = new ListViewItem(entry.Index.ToString());
            lvi.SubItems.Add(entry.Title);
            lvi.SubItems.Add(entry.Date);

            mainView.Items.Add(lvi);

            //Refreshing lisview by reloading all entries
            foreach (Form Faux in Application.OpenForms)
            {
                if (Faux.Name == "Form1")
                {
                    (Faux as Form1).loadEntries();
                }
            }

        }

        public TextEditor(ListView callingView,List<Entry> entri)
        {
            mainView = callingView as ListView;
            entries = entri;

            InitializeComponent();

            richTextBox1.SelectionFont = new Font("Tahoma", 14);
            metroTextBox1.Text = "<title>";
            cache = richTextBox1.Text;
            cacheTitle = metroTextBox1.Text;
            metroTextBox1.ForeColor = Color.FromArgb(100, 100, 100);

        }

        private void TextEditor_FormClosing(object sender, FormClosingEventArgs e)
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
                    else if(dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }

                if (saved == true)
                {
                    Write();
                }

            }
            

        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.Alt)
            {
                if (richTextBox1.SelectionFont.Size == 14)
                {
                    richTextBox1.SelectionFont = new Font("Tahoma", 7);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font("Tahoma", 14);
                }
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                Write();
                cache = richTextBox1.Text;
                cacheTitle = metroTextBox1.Text;
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

        private void metroTextBox1_Enter(object sender, EventArgs e)
        {
            if (metroTextBox1.Text ==  "<title>")
            {
                metroTextBox1.Text = "";
                metroTextBox1.ForeColor = Color.White;
            }
        }

        public string getTitle()
        {
            if (metroTextBox1.Text != "<title>")
                return metroTextBox1.Text;
            else
                return "";
        }

        public string getText()
        {
            return richTextBox1.Text;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Paint myPaint = new Paint();
            myPaint.ShowDialog();
        }
    }
}
