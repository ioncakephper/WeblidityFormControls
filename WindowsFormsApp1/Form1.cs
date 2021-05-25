using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string FileName { get; private set; }
        public string Content { get; set; }

        public Form1()
        {
            InitializeComponent();
            weblidityFormCloser1.Caption = Application.ProductName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScatterData();
            UpdateFormTitle();
        }

        private void ScatterData()
        {
            richTextBox1.Text = Content;
            SetControlsEnabled();
        }

        private void SetControlsEnabled()
        {
            // throw new NotImplementedException();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            weblidityFormCloser1.ConfirmFormClosing(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weblidityCommandManager1.Invoke(saveFileWeblidityCommand);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weblidityFileOpenSave1.SaveAs(FileName);
        }

        private void weblidityFileOpenSave1_AfterFileSave(object sender, WeblidityFormControls.FileOpenSaveEventArgs e)
        {
            if (!e.Errors)
            {
                FileName = e.FileName;
                weblidityFormCloser1.IsDirty = false;
                UpdateFormTitle();
            }
        }

        private void weblidityFileOpenSave1_FileSave(object sender, WeblidityFormControls.FileOpenSaveEventArgs e)
        {
            try
            {              
                string extension = System.IO.Path.GetExtension(e.FileName);
                string fileName = e.FileName;
                if (string.IsNullOrWhiteSpace(extension))
                {
                    fileName = System.IO.Path.ChangeExtension(fileName, ".txt");
                }
                GatherData();
                File.WriteAllText(fileName, Content);
            }
            catch (Exception)
            {
                e.Errors = true;
            }           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weblidityCommandManager1.Invoke(openFileWeblidityCommand);
            
        }

        private void weblidityFileOpenSave1_FileOpen(object sender, WeblidityFormControls.FileOpenSaveEventArgs e)
        {
            try
            {
                string fileName = e.FileName;
                string extention = System.IO.Path.GetExtension(fileName);

                if (string.IsNullOrWhiteSpace(extention))
                {
                    fileName = System.IO.Path.ChangeExtension(fileName, ".txt");
                }

                e.LoadedObject = File.ReadAllText(fileName);
            }
            catch (Exception)
            {
                e.Errors = true;
                throw;
            }
        }

        private void weblidityFileOpenSave1_BeforeFileSave(object sender, WeblidityFormControls.FileOpenSaveEventArgs e)
        {
            // e.Cancel = !weblidityFormCloser1.IsDirty;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            weblidityFormCloser1.IsDirty = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (weblidityFormCloser1.ConfirmationDialogResult == DialogResult.Yes)
            {
                GatherData();
                weblidityCommandManager1.Invoke(saveFileWeblidityCommand);
            }
        }

        private void GatherData()
        {
            Content = richTextBox1.Text;
        }

        private void saveFileWeblidityCommand_Command(object sender, EventArgs e)
        {
            var saveResult = weblidityFileOpenSave1.Save();
        }

        private void UpdateFormTitle()
        {
            string basename = string.IsNullOrWhiteSpace(FileName) ? "Untitled" : System.IO.Path.GetFileNameWithoutExtension(FileName);
            string appname = Application.ProductName;
            Text = string.Format(@"{0} - {1}", basename, appname);
        }

        private void openFileWeblidityCommand_Command(object sender, EventArgs e)
        {
            weblidityFileOpenSave1.Open();
        }

        private void weblidityFileOpenSave1_AfterFileOpen(object sender, WeblidityFormControls.FileOpenSaveEventArgs e)
        {
            if (!e.Errors)
            {
                FileName = e.FileName;
                Content = (string)e.LoadedObject;
                ScatterData();
                UpdateFormTitle();
                weblidityFormCloser1.IsDirty = false;
            }
        }
    }
}
