using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encyclopedia_2._0
{
    public class Entry
    {
        
        string title;
        public string Title { get { return title; } set { title = value; } }
        string text;
        public string Text { get { return text; } set { text = value; } }
        string date;
        public string Date { get { return date; } set { title = value; } }
        int index;
        public int Index { get { return index; } set { index = value; } }




        public Entry (string date,string title, string text,int index)
        {
            this.date = date;
            this.text = text;
            this.index = index;

            if (title == "<title>")
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Rtf = text;
                this.title = Regex.Replace(rtb.Text, "/nnn", " ");
            }
            else
                this.title = title;

        }

        public Entry(string title, string text, Control c, int index,string date)
        {
            this.text = text;
            this.index = index;
            this.date = date;

            if (title == "<title>")
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Rtf = text;
                this.title = (index + 1).ToString() + ") " + Regex.Replace(rtb.Text, "/nnn", " ");
            }
            else
                this.title = (index + 1).ToString() + ") " + title;

            MetroFramework.Controls.MetroTile m = new MetroFramework.Controls.MetroTile();
            m.Width = c.Width;
            m.Height = 20;
            m.Text = this.title;
            m.Location = new Point(0, index * (m.Height + 4));
            m.Style = MetroFramework.MetroColorStyle.Red;

            c.Controls.Add(m);

        }
    }
}
