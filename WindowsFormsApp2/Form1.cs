using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public event EventHandler<FilenameChangedEventArgs> FilenameChanged;
        private string _oldFilename;
        private string _fileName;

        public Form1()
        {
            InitializeComponent();
            FilenameChanged += Form1_FilenameChanged;
        }

        private void Form1_FilenameChanged(object sender, FilenameChangedEventArgs e)
        {
            weblidityFormCloser1.IsDirty = true;
        }

        private void ScatterData()
        {
            SetControlsEnabled();
        }

        private void SetControlsEnabled()
        {
            // throw new NotImplementedException();
        }

        public string FileName {
            get
            {
                return _fileName;
            }

            set {
                _oldFilename = _fileName;
                _fileName = value;
                OnFilenameChanged(_oldFilename, _fileName);
            }
        }

        protected void OnFilenameChanged(string oldFilename, string fileName)
        {
            if (FilenameChanged != null)
            {
                FilenameChangedEventArgs e = new FilenameChangedEventArgs() { OldFilename = oldFilename, NewFilename = fileName };
                FilenameChanged(this, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var d = new Dialog1();
            d.FileName = FileName;
            if (d.ShowDialog() == DialogResult.OK)
            {
                FileName = d.FileName;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            weblidityFormCloser1.ConfirmFormClosing(sender, e);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (weblidityFormCloser1.ConfirmationDialogResult == DialogResult.Yes)
            {
                GatherData();
                MessageBox.Show("Saving data into a file");
                // Save data into a file
            }
        }

        private void GatherData()
        {
            FileName = FileName;
        }
    }

    public class FilenameChangedEventArgs: EventArgs
    {
        public FilenameChangedEventArgs()
        {
        }

        public string OldFilename { get; set; }
        public string NewFilename { get; set; }
    }
}
