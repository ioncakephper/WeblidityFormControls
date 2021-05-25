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
    public partial class Dialog1 : Form
    {
        public string FileName { get; set; }

        public Dialog1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gather data from controls.
        /// </summary>
        private void GatherData()
        {
            FileName = textBox1.Text;
        }

        /// <summary>
        /// Scatter dialog properties values into dialog controls
        /// </summary>
        private void ScatterData()
        {
            textBox1.Text = FileName;
            SetControlsEnabled();
        }

        /// <summary>
        /// Set Enabled control value based on conditions.
        /// </summary>
        private void SetControlsEnabled()
        {
            okButton.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog();
            d.FileName = textBox1.Text;
            if (d.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = d.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            weblidityFormCloser1.IsDirty = true;
            SetControlsEnabled();
        }

        private void Dialog1_Load(object sender, EventArgs e)
        {
            ScatterData();
            weblidityFormCloser1.IsDirty = false;
        }

        private void Dialog1_FormClosing(object sender, FormClosingEventArgs e)
        {
            weblidityFormCloser1.ConfirmFormClosing(sender, e);
        }

        private void Dialog1_FormClosed(object sender, FormClosedEventArgs e)
        {
            GatherData();
        }
    }
}
