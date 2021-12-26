using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thread
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Open = new OpenFileDialog();
            Save = new SaveFileDialog();
            BackgroundWorker worker = new BackgroundWorker();
        }

        private void btnBrowseFrom_Click(object sender, EventArgs e)
        {
            FileText = "";
            if(Open.ShowDialog() == DialogResult.OK)
            {
                FileText = File.ReadAllText(Open.FileName);
                textBox1.Text = Open.FileName;
            }
        }

        private void btnBrowseTo_Click(object sender, EventArgs e)
        {
            if (Open.ShowDialog() == DialogResult.OK)
            {                
                textBox2.Text = Open.FileName;
            }
        }

        private void Progress(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100) progressBar1.Value++;
            else 
            { 
                Timer.Stop();
                progressBar1.Value = 0;
                MessageBox.Show("File Copied Successfully");
            }
        }

        private void Tick()
        {
            Timer = new Timer();
            Timer.Start();
            Timer.Enabled = true;
            Timer.Interval = 1000;
            progressBar1.Maximum = 100;
            Timer.Tick += new EventHandler(Progress);
        }

        private void Copy()
        {
            File.WriteAllText(Open.FileName, FileText);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Copy();
            Tick();
        }

        OpenFileDialog Open { get; set; }
        SaveFileDialog Save { get; set; }
        Timer Timer { get; set; }
        string FileText { get; set; }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            Timer.Stop();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            Timer.Start();
        }
    }
}
