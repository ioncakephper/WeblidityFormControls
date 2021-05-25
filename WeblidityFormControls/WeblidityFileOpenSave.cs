//=============================================
// File Name: WeblidityFileOpenSave.cs
// Code Formatted on: 5/21/2021 7:35:46 PM
//
// Author: Ion Gireada 
//=============================================

namespace WeblidityFormControls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="WeblidityFileOpenSave" />.
    /// </summary>
    public partial class WeblidityFileOpenSave : Component
    {
        /// <summary>
        /// Defines the e.
        /// </summary>
        private FileOpenSaveEventArgs e;

        /// <summary>
        /// Defines the FileSave.
        /// </summary>
        /// 
        [Description("Occurs when content needs to be saved")]
        public event EventHandler<FileOpenSaveEventArgs> FileSave;

        /// <summary>
        /// Defines the BeforeFileSave.
        /// </summary>
        /// 
        [Description("Occurs before saving the content")]
        public event EventHandler<FileOpenSaveEventArgs> BeforeFileSave;

        /// <summary>
        /// Defines the AfterFileSave.
        /// </summary>
        /// 
        [Description("Occurs after saving the content")]
        public event EventHandler<FileOpenSaveEventArgs> AfterFileSave;

        /// <summary>
        /// Defines the FileOpen.
        /// </summary>
        /// 
        [Description("Occurs when content needs to be open")]
        public event EventHandler<FileOpenSaveEventArgs> FileOpen;

        /// <summary>
        /// Defines the BeforeFileOpen.
        /// </summary>
        /// 
        [Description("Occurs before opening the content")]
        public event EventHandler<FileOpenSaveEventArgs> BeforeFileOpen;

        /// <summary>
        /// Defines the AfterFileOpen.
        /// </summary>
        /// 
        [Description("Occurs after opening the content")]
        public event EventHandler<FileOpenSaveEventArgs> AfterFileOpen;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeblidityFileOpenSave"/> class.
        /// </summary>
        public WeblidityFileOpenSave()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeblidityFileOpenSave"/> class.
        /// </summary>
        /// <param name="container">The container<see cref="IContainer"/>.</param>
        public WeblidityFileOpenSave(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Saves content in a specified filename.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="FileOpenSaveResult"/>.</returns>
        public FileOpenSaveResult SaveAs(string fileName)
        {
            FileName = fileName;
            return SaveAs();
        }

        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        /// 
        [DefaultValue("")]
        [Description("The filename where content is saved to or opened from")]
        public string FileName { get; set; } = "";

        /// <summary>
        /// Gets or sets the SaveFileDialog.
        /// </summary>
        /// 
        [Description("The dialog to use when choosing a filename to save into")]
        public SaveFileDialog SaveFileDialog { get; set; } = new SaveFileDialog();

        /// <summary>
        /// Gets or sets the OpenFileDialog.
        /// </summary>
        /// 
        [Description("The dialog to use when choosing a filename to open")]
        public OpenFileDialog OpenFileDialog { get; set; } = new OpenFileDialog();

        /// <summary>
        /// Save content in current filename or ask for a filename.
        /// </summary>
        /// <returns>The <see cref="FileOpenSaveResult"/>.</returns>
        /// 
        public FileOpenSaveResult Save()
        {
            if (!FileNameExists())
            {
                return SaveAs();
            }
            OnFileSave(FileName);
            return e.Errors ? FileOpenSaveResult.Failure : FileOpenSaveResult.Success;
        }

        /// <summary>
        /// Invokes the FileSave event code. The code executes unless cancelled in BeforeFileSave event.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        protected void OnFileSave(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }
            e = new FileOpenSaveEventArgs()
            {
                FileName = fileName
            };

            if (FileSave != null)
            {

                if (BeforeFileSave != null)
                {
                    BeforeFileSave(this, e);
                }

                if (!e.Cancel)
                {
                    FileSave(this, e);
                }

                if (AfterFileSave != null)
                {
                    AfterFileSave(this, e);
                }
            }
        }




        /// <summary>
        /// Open file content and store it in LoadedObject property of the FileOpenSaveEventArgs object.
        /// </summary>
        /// <returns>The <see cref="FileOpenSaveResult"/>.</returns>
        public FileOpenSaveResult Open()
        {
            var d = new OpenFileDialog();
            if (d.ShowDialog() == DialogResult.OK)
            {
                if (Save() == FileOpenSaveResult.Success)
                {
                    FileName = d.FileName;
                    if (FileNameExists(true))
                    {
                        OnFileOpen(FileName);

                        return e.Errors ? FileOpenSaveResult.Failure : FileOpenSaveResult.Success;
                    }
                }
            }
            return FileOpenSaveResult.Cancel;
        }

        /// <summary>
        /// The OnFileOpen.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        protected void OnFileOpen(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }

            e = new FileOpenSaveEventArgs() { FileName = fileName };
            if (FileOpen != null)
            {
                if (BeforeFileOpen != null)
                {
                    BeforeFileOpen(this, e);
                }
                if (!e.Cancel)
                {
                    FileOpen(this, e);
                }
                if (AfterFileOpen != null)
                {
                    AfterFileOpen(this, e);
                }
            }
        }

        /// <summary>
        /// The FileNameExists.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool FileNameExists()
        {
            return FileNameExists(false);
        }

        /// <summary>
        /// The FileNameExists.
        /// </summary>
        /// <param name="isForOpen">The isForOpen<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool FileNameExists(bool isForOpen)
        {
            bool r = !string.IsNullOrWhiteSpace(FileName);
            return isForOpen ? r && System.IO.File.Exists(FileName) : r;
        }

        /// <summary>
        /// The SaveAs.
        /// </summary>
        /// <returns>The <see cref="FileOpenSaveResult"/>.</returns>
        public FileOpenSaveResult SaveAs()
        {
            SaveFileDialog d = new SaveFileDialog();
            d.FileName = FileName;
            if (d.ShowDialog() == DialogResult.OK)
            {
                return Save(d.FileName);
            }
            return FileOpenSaveResult.Cancel;
        }

        /// <summary>
        /// The Save.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="FileOpenSaveResult"/>.</returns>
        private FileOpenSaveResult Save(string fileName)
        {
            FileName = fileName;
            return Save();
        }
    }

    /// <summary>
    /// Defines the <see cref="FileOpenSaveEventArgs" />.
    /// </summary>
    public class FileOpenSaveEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Cancel.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Errors.
        /// </summary>
        public bool Errors { get; set; }

        /// <summary>
        /// Gets or sets the LoadedObject.
        /// </summary>
        public object LoadedObject { get; set; }
    }

    /// <summary>
    /// Defines the FileOpenSaveResult.
    /// </summary>
    public enum FileOpenSaveResult
    {
        /// <summary>
        /// Defines the Success.
        /// </summary>
        Success,

        /// <summary>
        /// Defines the Failure.
        /// </summary>
        Failure,

        /// <summary>
        /// Defines the Cancel.
        /// </summary>
        Cancel
    }
}
