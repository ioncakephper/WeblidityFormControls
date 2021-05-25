//=============================================
// File Name: Form1.cs
// Code Formatted on: 5/25/2021 9:46:54 AM
//
// Author: Ion Gireada 
//=============================================

namespace WindowsFormsApp3
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Form1" />.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Defines the _sidebarCount.
        /// </summary>
        private int _sidebarCount;

        /// <summary>
        /// Defines the _topicCount.
        /// </summary>
        private int _topicCount;

        /// <summary>
        /// Gets or sets the DocumentationProject.
        /// </summary>
        public DocumentationProject DocumentationProject { get; set; }

        /// <summary>
        /// Gets or sets the DocusaurusLocation.
        /// </summary>
        public string DocusaurusLocation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            DocumentationProject = new DocumentationProject();
            DocumentationProject.Sidebars.Add(new DocumentationSidebar() { Title = "Docs" });
        }

        /// <summary>
        /// The ScatterData.
        /// </summary>
        public void ScatterData()
        {
            PopulateTreeView();
            SetControlsEnabled();
        }

        /// <summary>
        /// The PopulateTreeView.
        /// </summary>
        private void PopulateTreeView()
        {
            treeView1.Nodes.Clear();
            DocumentationProjectNode projectNode = NewDocumentationProjectNode(DocumentationProject);
            treeView1.Nodes.Add(projectNode);
        }

        /// <summary>
        /// The NewDocumentationProjectNode.
        /// </summary>
        /// <param name="documentationProject">The documentationProject<see cref="DocumentationProject"/>.</param>
        /// <returns>The <see cref="DocumentationProjectNode"/>.</returns>
        private DocumentationProjectNode NewDocumentationProjectNode(DocumentationProject documentationProject)
        {
            if (documentationProject == null)
            {
                throw new ArgumentNullException(nameof(documentationProject));
            }

            DocumentationProjectNode projectNode = new DocumentationProjectNode();
            projectNode.Text = string.IsNullOrWhiteSpace(documentationProject.Title) ? "Untitled" : documentationProject.Title;
            projectNode.Tag = documentationProject;
            CreateSidebarChildren(documentationProject.Sidebars, projectNode);
            return projectNode;
        }

        /// <summary>
        /// The CreateSidebarChildren.
        /// </summary>
        /// <param name="sidebars">The sidebars<see cref="List{DocumentationSidebar}"/>.</param>
        /// <param name="projectNode">The projectNode<see cref="DocumentationProjectNode"/>.</param>
        private void CreateSidebarChildren(List<DocumentationSidebar> sidebars, DocumentationProjectNode projectNode)
        {
            if (sidebars == null)
            {
                return;
            }

            if (projectNode == null)
            {
                return;
            }

            foreach (var sidebar in sidebars)
            {
                DocumentationSidebarNode sidebarNode = NewDocumentationSidebarNode(sidebar);
                projectNode.Nodes.Add(sidebarNode);
            }
        }

        /// <summary>
        /// The NewDocumentationSidebarNode.
        /// </summary>
        /// <param name="sidebar">The sidebar<see cref="DocumentationSidebar"/>.</param>
        /// <returns>The <see cref="DocumentationSidebarNode"/>.</returns>
        private DocumentationSidebarNode NewDocumentationSidebarNode(DocumentationSidebar sidebar)
        {
            DocumentationSidebarNode sidebarNode = new DocumentationSidebarNode();
            sidebarNode.Text = string.IsNullOrWhiteSpace(sidebar.Title) ? ("Untitled " + (_sidebarCount++).ToString()) : sidebar.Title;
            sidebarNode.Tag = sidebar;
            CreateTopicChildren(sidebar.Topics, sidebarNode);

            return sidebarNode;
        }

        /// <summary>
        /// The CreateTopicChildren.
        /// </summary>
        /// <param name="topics">The topics<see cref="List{DocumentationTopic}"/>.</param>
        /// <param name="parentNode">The parentNode<see cref="TreeNode"/>.</param>
        private void CreateTopicChildren(List<DocumentationTopic> topics, TreeNode parentNode)
        {
            if (topics == null)
            {
                throw new ArgumentNullException(nameof(topics));
            }

            if (parentNode == null)
            {
                throw new ArgumentNullException(nameof(parentNode));
            }

            foreach (var topic in topics)
            {
                DocumentationTopicNode topicNode = NewDocumentationTopicNode(topic);
                parentNode.Nodes.Add(topicNode);
            }
        }

        /// <summary>
        /// The NewDocumentationTopicNode.
        /// </summary>
        /// <param name="topic">The topic<see cref="DocumentationTopic"/>.</param>
        /// <returns>The <see cref="DocumentationTopicNode"/>.</returns>
        private DocumentationTopicNode NewDocumentationTopicNode(DocumentationTopic topic)
        {
            DocumentationTopicNode topicNode = new DocumentationTopicNode();
            topicNode.Text = string.IsNullOrWhiteSpace(topic.Title) ? "Untitled" + _topicCount++.ToString() : topic.Title;
            topicNode.Tag = topic;
            CreateTopicChildren(topic.Topics, topicNode);

            return topicNode;
        }

        /// <summary>
        /// The SetControlsEnabled.
        /// </summary>
        private void SetControlsEnabled()
        {
            UpdateFormTitle();
        }

        /// <summary>
        /// The GatherData.
        /// </summary>
        public void GatherData()
        {
        }

        /// <summary>
        /// The UpdateFormTitle.
        /// </summary>
        public void UpdateFormTitle()
        {
            string basename = "Untitled";
            string appname = Application.ProductName;
            Text = string.Format(@"{0} - {1}", basename, appname);
        }

        /// <summary>
        /// The Form1_Load.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ScatterData();
        }

        /// <summary>
        /// The buildSidebarsFileWeblidityCommand_Command.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void buildSidebarsFileWeblidityCommand_Command(object sender, EventArgs e)
        {

            string docusaurusLocation = GetDocusaurusLocation();
            if (!string.IsNullOrEmpty(docusaurusLocation))
            {
                string docusaurusWebsiteFolder = System.IO.Path.Combine(docusaurusLocation, "website");

                string sidebarsFilename = System.IO.Path.Combine(docusaurusWebsiteFolder, "sidebars.js");
                string sidebarsContent = "{}";
                File.WriteAllText(sidebarsFilename, sidebarsContent);

                MessageBox.Show(string.Format(@"Created sidebars file at: {0}", sidebarsFilename));
            }
        }

        /// <summary>
        /// The GetDocusaurusLocation.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetDocusaurusLocation()
        {
            if (string.IsNullOrWhiteSpace(DocusaurusLocation))
            {
                var d = new ChooseDocusaurusLocationDialog();
                d.DocusaurusLocation = DocusaurusLocation;
                if (d.ShowDialog() == DialogResult.Cancel)
                {
                    return string.Empty;
                }
                DocusaurusLocation = d.DocusaurusLocation;
            }
            return DocusaurusLocation;
        }

        /// <summary>
        /// The buildToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weblidityCommandManager1.Invoke(buildSidebarsFileWeblidityCommand);
        }

        /// <summary>
        /// The newTopicToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void newTopicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var topicNode = NewDocumentationTopicNode(NewTopic());
            // treeView1.Nodes.Add(topicNode);
            treeView1.SelectedNode.Nodes.Add(topicNode);
            treeView1.SelectedNode = topicNode;
            weblidityFormCloser1.IsDirty = true;
        }

        /// <summary>
        /// The NewTopic.
        /// </summary>
        /// <returns>The <see cref="DocumentationTopic"/>.</returns>
        private DocumentationTopic NewTopic()
        {
            var topic = new DocumentationTopic() { Title = "Topic" + _topicCount++.ToString() };
            return topic;
        }

        /// <summary>
        /// The newTopicToolStripSplitButton_ButtonClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void newTopicToolStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            newTopicToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// The Form1_FormClosing.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="FormClosingEventArgs"/>.</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            weblidityFormCloser1.ConfirmFormClosing(sender, e);
        }

        /// <summary>
        /// The Form1_FormClosed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="FormClosedEventArgs"/>.</param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (weblidityFormCloser1.ConfirmationDialogResult == DialogResult.Yes)
            {
                GatherData();
                weblidityFileOpenSave1.Save();
            }
        }
    }
}
