using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class ChooseDocusaurusLocationDialog : Form
    {
        public string DocusaurusLocation { get; set; }

        public ChooseDocusaurusLocationDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gather data from controls.
        /// </summary>
        private void GatherData()
        {
            DocusaurusLocation = textBox1.Text;
        }

        /// <summary>
        /// Scatter dialog properties values into dialog controls
        /// </summary>
        private void ScatterData()
        {
            textBox1.Text = DocusaurusLocation;
            SetControlsEnabled();
        }

        /// <summary>
        /// Set Enabled control value based on conditions.
        /// </summary>
        private void SetControlsEnabled()
        {
            okButton.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text) && System.IO.Directory.Exists(textBox1.Text);
        }

        private void ChooseDocusaurusLocationDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            weblidityFormCloser1.ConfirmFormClosing(sender, e);
        }

        private void ChooseDocusaurusLocationDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            GatherData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            weblidityFormCloser1.IsDirty = true;
            SetControlsEnabled();
        }

        private void ChooseDocusaurusLocationDialog_Load(object sender, EventArgs e)
        {
            ScatterData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox1.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
