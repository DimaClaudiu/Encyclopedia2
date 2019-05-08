namespace Encyclopedia_2._0
{
    partial class TextEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MetroSuite.MetroTextBox.MainColorScheme mainColorScheme1 = new MetroSuite.MetroTextBox.MainColorScheme();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextEditor));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.metroTextBox1 = new MetroSuite.MetroTextBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.richTextBox1.ForeColor = System.Drawing.Color.Silver;
            this.richTextBox1.Location = new System.Drawing.Point(0, 63);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(600, 437);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.BanChars = false;
            mainColorScheme1.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(240)))));
            mainColorScheme1.BorderColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            mainColorScheme1.MainColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.metroTextBox1.ColorScheme = mainColorScheme1;
            this.metroTextBox1.DefaultText = null;
            this.metroTextBox1.DefaultTextColor = System.Drawing.Color.LightGray;
            this.metroTextBox1.DefaultTextNormalForeColor = System.Drawing.SystemColors.ControlText;
            this.metroTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.metroTextBox1.ForeColor = System.Drawing.Color.Black;
            this.metroTextBox1.IllegalChars = ((System.Collections.Generic.List<char>)(resources.GetObject("metroTextBox1.IllegalChars")));
            this.metroTextBox1.Location = new System.Drawing.Point(415, 34);
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.Size = new System.Drawing.Size(174, 23);
            this.metroTextBox1.Style = MetroSuite.Design.Style.Dark;
            this.metroTextBox1.TabIndex = 1;
            this.metroTextBox1.Text = "metroTextBox1";
            this.metroTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.metroTextBox1.UseDefaultText = false;
            this.metroTextBox1.Enter += new System.EventHandler(this.metroTextBox1_Enter);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(385, 34);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(24, 23);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "P";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroTextBox1);
            this.Controls.Add(this.richTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TextEditor";
            this.Text = "Quick Entry";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextEditor_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSuite.MetroTextBox metroTextBox1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}