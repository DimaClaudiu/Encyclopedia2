using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encyclopedia_2._0
{
    public partial class Paint : MetroFramework.Forms.MetroForm
    {
        public void MyKeyPressEventHandler(Object sender, KeyEventArgs e)
        {

        }
       
        public Paint()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            //(panel1 as Control).KeyPress += new KeyPressEventHandler(MyKeyPressEventHandler);
        }

        bool startPaint = false;
        Graphics g;
        string name = "iamge";
        //nullable int for storing Null value  
        int? initX = null;
        int? initY = null;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint)
            {
                //Setting the Pen BackColor and line Width  
                Pen p = new Pen(Color.Red, 2);
                //Drawing the line.  
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
            }

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
        }

        private void Paint_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Save the image?", "Don't forget me :(", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                using (Bitmap graphicSurface = new Bitmap(panel1.Width, panel1.Height))
                {
                    using (StreamWriter bitmapWriter = new StreamWriter(name + ".png"))
                    {

                        panel1.DrawToBitmap(graphicSurface, new Rectangle(0, 0, panel1.Width, panel1.Height));
                        graphicSurface.Save(bitmapWriter.BaseStream, ImageFormat.Jpeg);
                    }
                }

            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
