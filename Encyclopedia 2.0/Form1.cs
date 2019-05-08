using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Encyclopedia_2._0
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        static string ResourcesPath = GlobalConstants.ResourcesPath; //All around

        DateTime startDate = DateTime.Now; //All around


        #region Program Specific Methods (1.0)

        #region Entries (1.1)


        List<Entry> entries = new List<Entry>(); //Quick Entry

        string[] ReturnFileNames()
        {
            DirectoryInfo d = new DirectoryInfo(ChartHistoryPath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(); //Getting Text files
            string[] fileNames = new string[Files.Length];
            int i = 0;
            foreach (FileInfo file in Files)
            {
                fileNames[i] = file.Name;
                i++;
            }

            return fileNames;
        }

        private void loadListFromEntries()
        {
            listView1.Items.Clear();
            foreach (Entry entry in entries)
            {
                ListViewItem lvi = new ListViewItem(entry.Index.ToString());
                lvi.SubItems.Add(entry.Title);
                lvi.SubItems.Add(entry.Date);
                lvi.ToolTipText = entry.Text;

                listView1.Items.Add(lvi);
            }
        }

        public void loadEntries()
        {
            entries.Clear();

            //Reading from DataBase
            using (SqlConnection connection = new SqlConnection(GlobalConstants.DatabaseConnectionPath))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Entry";
                connection.Open();
                SqlDataReader sdr = command.ExecuteReader();

                while (sdr.Read())
                {
                    Entry entry = new Entry(((DateTime)sdr[1]).ToString("dd/MM/yy"), sdr[2].ToString(), sdr[3].ToString(), int.Parse(sdr[0].ToString()));
                    entries.Add(entry);

                }
                sdr.Close();
                connection.Close();
            }

            loadListFromEntries();

        }



        #endregion Entries

        #region Activity Monitor Tick (1.2)

        static string ChartHistoryPath = ResourcesPath + @"\Chart History";
        List<Process> processes = new List<Process>(); //Activity Monitor
        List<Label> labels = new List<Label>(); //Activity Monitor


        #region UpTime (1.21)
        [DllImport("kernel32")]
        extern static UInt64 GetTickCount64();
        public static TimeSpan GetUpTime()
        {
            return TimeSpan.FromMilliseconds(GetTickCount64());
        }
        #endregion UpTime

        #region GetForegroundProcessName (1.22)
        // The GetForegroundWindow function returns a handle to the foreground window
        // (the window  with which the user is currently working).
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // The GetWindowThreadProcessId function retrieves the identifier of the thread
        // that created the specified window and, optionally, the identifier of the
        // process that created the window.
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // Returns the name of the process owning the foreground window.
        private string GetForegroundProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();

            // The foreground window can be NULL in certain circumstances, 
            // such as when a window is losing activation.
            if (hwnd == null)
                return "Unknown";

            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);

            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.Id == pid)
                    return p.ProcessName;
            }

            return "Unknown";
        }
        #endregion GetForegroundProcessName

        private string FormatProcess(string p)
        {
            string aux = p;
            if (aux.Length > 12)
                aux = aux.Remove(12);
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(aux);
        }

        private void loadComboBox(bool side)
        {
            comboBox1.Items.Clear();
            foreach (string f in ReturnFileNames())
            {
                string item = f.Remove(f.Length - 4);
                comboBox1.Items.Add(item);
                
                if(item == startDate.ToLongDateString() && side) 
                {
                    comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                }
            }
        }

        private void loadChart(string date)
        {
            string[] text = File.ReadAllLines(ChartHistoryPath + date + ".txt");
            for (int i = 0; i < text.Length; i++)
            {
                string[] aux = text[i].Split('|');

            }
        }

        DateTime time = new DateTime();
        private void timer1_Tick(object sender, EventArgs e)
        {

            string s = GetForegroundProcessName();
            bool exists = false;
            Random r = new Random();



            foreach (Process p in processes)
            {
                if (p.compareTo(s))
                {
                    exists = true;
                    p.Minutes += DateTime.Now - time;

                    break;
                }
            }

            if (!exists)
            {
                Process p = new Process(s);
                processes.Add(p);

                Label l = new Label();
                l.BackColor = p.Color;
                l.Text = FormatProcess(p.Name);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.ForeColor = p.fontColor;
                labels.Add(l);
                tabPage1.Controls.Add(l);

            }

            if (comboBox1.Text == startDate.ToLongDateString())
            {
                metroPieChart1.Segments.Clear();
                foreach (Process p in processes)
                {
                    MetroSuite.MetroPieChartSegment seg = new MetroSuite.MetroPieChartSegment();
                    seg.Name = p.Name;
                    seg.FillColor = p.Color;
                    seg.Value = (int)TimeSpan.FromTicks(p.Minutes.Ticks).TotalMilliseconds;
                    metroPieChart1.Segments.Add(seg);
                }
                toolTip1.SetToolTip(metroPieChart1, "System Uptime: " + GetUpTime().ToString().Remove(GetUpTime().ToString().Length - 8));


                int labelX, labelY;
                for (int i = 0; i < labels.Count; i++)
                {

                    labelX = (i) % (tabPage1.Width / labels[i].Width - 0) * labels[i].Width + labels[i].Width / 2;

                    labelY = tabPage1.Height - labels[i].Height - 20 - (i - 1) / (tabPage1.Width / labels[i].Width - 1) * labels[i].Height;

                    labels[i].Location = new Point(labelX, labelY);

                    toolTip1.SetToolTip(labels[i], processes[i].Minutes.ToString("HH:mm:ss"));
                }

            }


            time = DateTime.Now;
        }

        public void pauseTimer1()
        {
            timer1.Enabled = !timer1.Enabled;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {

            loadComboBox(false);
            if (DateTime.Now.Day == startDate.Day) //if it's the same day, else, we change to a new day
            {
                try
                {  
                    StreamWriter sw = new StreamWriter(ChartHistoryPath + "\\" + startDate.ToLongDateString() + ".txt");
                    //Console.WriteLine(ChartHistoryPath + "\\" + startDate.ToLongDateString() + ".txt");
                    foreach (Process p in processes)
                    {
                        sw.WriteLine(p.Name + "|" + ColorTranslator.ToHtml(p.Color) + "|" + ColorTranslator.ToHtml(p.fontColor) + "|" + p.Minutes.Ticks);
                    }
                    sw.Close();
                }
                catch { }


            }
            else //a new day, we reset
            {
                startDate = DateTime.Now;

                processes.Clear();
                labels.Clear();
                metroPieChart1.Segments.Clear();
            }

        }

        List<Process> currentProcesses = new List<Process>();
        List<Label> currentLabels = new List<Label>();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                metroPieChart1.Segments.Clear();
                currentProcesses.Clear();
                currentLabels.Clear();
                List<Label> LabelsToRemove = new List<Label>();
                foreach (Control item in tabPage1.Controls)
                {
                    if (item.GetType() == typeof(Label))
                    {
                        LabelsToRemove.Add((Label)item);
                    }

                    foreach (var label in LabelsToRemove)
                    {
                        this.tabPage1.Controls.Remove(label);
                        label.Dispose();
                    }

                }
                string[] text = File.ReadAllLines(ChartHistoryPath + "\\" + comboBox1.SelectedItem + ".txt");
                foreach (string line in text)
                {
                    string[] aux = line.Split('|');
                    MetroSuite.MetroPieChartSegment seg = new MetroSuite.MetroPieChartSegment();
                    seg.Name = aux[0];
                    seg.FillColor = ColorTranslator.FromHtml(aux[1]);

                    seg.Value = int.Parse(aux[3]);
                    metroPieChart1.Segments.Add(seg);

                    Label l = new Label();
                    l.BackColor = ColorTranslator.FromHtml(aux[1]);
                    l.Text = FormatProcess(aux[0]);
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    l.ForeColor = ColorTranslator.FromHtml(aux[2]);
                    currentLabels.Add(l);
                    tabPage1.Controls.Add(l);

                    int labelX, labelY;
                    for (int i = 0; i < currentLabels.Count; i++)
                    {

                        labelX = (i) % (tabPage1.Width / currentLabels[i].Width - 0) * currentLabels[i].Width + currentLabels[i].Width / 2;

                        labelY = tabPage1.Height - currentLabels[i].Height - 20 - (i - 1) / (tabPage1.Width / currentLabels[i].Width - 1) * currentLabels[i].Height;

                        currentLabels[i].Location = new Point(labelX, labelY);

                        //toolTip1.SetToolTip(currentLabels[i], processes[i].Minutes.ToString("HH:mm:ss"));
                    }

                }
            }
            catch { }
           


        }

        #endregion Activity Monitor Tick

        #endregion Program Specific Methods

        #region Natural Program Behaviour Methods (2.0)

        #region Tray (2.1)

        private void minimizeToTray()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
            notifyIcon1.Visible = true;
        }

        private void maximizeFromTray()
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                minimizeToTray();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            maximizeFromTray();
        }

        #endregion Tray

        #region ListView (2.2)

        private ListViewColumnSorter lvwColumnSorter = null; //List view sorter(object)

        private void deleteLine(int index)
        {

            //StreamWriter s = new StreamWriter(EntryPath);

            //for(int i = 0;i<entries.Count;i++)
            //{
            //    if(i!=index)
            //    {
            //        s.WriteLine(entries[i].Date);
            //        s.WriteLine(entries[i].Title);
            //        s.WriteLine(entries[i].Text + " <end>");

            //        s.WriteLine();
            //    }
            //}
            //s.Close();

            using (SqlConnection connection = new SqlConnection(GlobalConstants.DatabaseConnectionPath))
            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = "DELETE FROM Entry Where Id = '" + index.ToString() + "'";

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private List<int> searchEntries(string s)
        {
            List<int> indexes = new List<int>();


            foreach (Entry e in entries)
            {
                if (e.Title.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    indexes.Insert(0, e.Index);
                }
                else if (e.Text.Contains(s))
                {
                    indexes.Add(e.Index);
                }
            }

            return indexes;
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.ColumnToSort)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.ColumnToSort = e.Column;
                lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();

        } //List view CollumClick Handler

        private void button_addNew_Click(object sender, EventArgs e)
        {
            TextEditor myTextPopUp = new TextEditor(this.listView1, entries);
            myTextPopUp.ShowDialog();
            metroTextBox1.Text = "";
        } //Add New Button Behavoiur

        private void button_remove_Click(object sender, EventArgs e)
        {
            int index = int.Parse(listView1.SelectedItems[0].Text);
            // entries.RemoveAt(index);
            deleteLine(index);

            //listView1.Items.RemoveAt(index);
            //entries.Clear();
            //listView1.Items.Clear();
            loadEntries();


        } //Remove Button Behaviour

        private void listView1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = listView1.SelectedItems[0].Index;
            //Console.WriteLine(listView1.SelectedItems[0].SubItems.ToString());
            TextViewer myViewerPopUp = new TextViewer(entries, selectedItem);
            myViewerPopUp.ShowDialog();
        }

        #endregion ListView

        #region Global Hotkeys (2.3)
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int MYACTION_HOTKEY_ID = 1;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            {
                // My hotkey has been typed
                this.Visible = !this.Visible;

            }
            base.WndProc(ref m);
        }

        #endregion Global Hotkeys (2.3)



        #endregion NaturalProgramBehaviourMethods


        public Form1()
        {
            InitializeComponent();

            //SetStartup();
            loadEntries();

            notifyIcon1.Visible = false;
            timer1.Enabled = true;
            timer1.Interval = 100;
            timer1.Start();
            comboBox1.Visible = false;

            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.AutoArrange = true;

            lvwColumnSorter._SortModifier = ListViewColumnSorter.SortModifiers.SortByText;

            loadComboBox(true);

            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 6, (int)Keys.X);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //    DirectoryInfo d = new DirectoryInfo(ChartHistoryPath);
            //    FileInfo[] Files = d.GetFiles();


            //    foreach (FileInfo file in Files)
            //    {
            //        comboBox1.Items.Add(file.ToString().Remove(file.ToString().Length - 4));
            //    }

            //    if (comboBox1.Items.Contains("Today"))
            //    {
            //        comboBox1.Items.Insert(0, "Today");
            //    }
            //    else
            //    {
            //        File.CreateText(ChartHistoryPath + "Today.txt");
            //        comboBox1.Items.Insert(0, "Today");
            //    }

            //    comboBox1.SelectedIndex = 0;
        }


        private void metroTextBox1_Enter(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "Search")
            {
                metroTextBox1.Text = "";
                metroTextBox1.ForeColor = Color.White;
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {

            if (metroTextBox1.Text != "")
            {
                List<int> indexes = new List<int>();

                indexes = searchEntries(metroTextBox1.Text);

                listView1.Items.Clear();

                foreach (int index in indexes)
                {
                    ListViewItem lvi = new ListViewItem(entries[index - 1].Index.ToString());
                    lvi.SubItems.Add(entries[index - 1].Title);
                    lvi.SubItems.Add(entries[index - 1].Date);

                    listView1.Items.Add(lvi);

                }
            }
            else
            {
                listView1.Items.Clear();
                loadListFromEntries();
            }

        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                button_addNew.PerformClick();
            }
        }


        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                using (var headerFont = new Font("Tahoma", 9))
                {
                    e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Options myOptions = new Options(timer1,timer2);
            myOptions.ShowDialog();
        }
    }
}
