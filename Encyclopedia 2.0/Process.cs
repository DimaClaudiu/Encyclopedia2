using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encyclopedia_2._0
{
    class Process
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
  
        DateTime minutes;
        public DateTime Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
            }
        }

        Color color;
        public Color Color
        {
            get { return color; } set { color = value; }
        }

        public Color fontColor;
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        public Process(string name)
        {
            this.name = name;
            this.minutes = new DateTime();

            Random r = new Random();
            
           

            if (name == "Encyclopedia 2.0.vshost" || name == "Encyclopedia 2.0")
                color = Color.FromArgb(2, 20, 57);
            else if (name == "chrome")
            {
                color = Color.FromArgb(128, 21, 21);
                fontColor = Color.FromArgb(10, 10, 10);
            }
            else if (name == "Steam")
                color = Color.FromArgb(20, 20, 20);
            else if (name == "explorer")
                color = Color.Gold;
            else if (name == "idle")
                color = Color.BlueViolet;
            else if (name == "Discord")
                color = Color.DarkSlateBlue;
            else
            {
                color = Color.FromArgb(r.Next(0, 100), r.Next(0, 100), r.Next(0, 100));
                
            }

            fontColor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);

        }

        public bool compareTo(string s)
        {
            return name == s;
        }
    }
}
