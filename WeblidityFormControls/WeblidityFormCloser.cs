//=============================================
// File Name: WeblidityFormCloser.cs
// Code Formatted on: 5/21/2021 10:28:42 AM
//
// Author: Ion Gireada 
//=============================================

namespace WeblidityFormControls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="WeblidityFormCloser" />.
    /// </summary>
    public partial class WeblidityFormCloser : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeblidityFormCloser"/> class.
        /// </summary>
        public WeblidityFormCloser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeblidityFormCloser"/> class.
        /// </summary>
        /// <param name="container">The container<see cref="IContainer"/>.</param>
        public WeblidityFormCloser(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the ConfirmationDialogResult.
        /// </summary>
        [Browsable(false)]
        public DialogResult ConfirmationDialogResult { get; set; }

        /// <summary>
        /// Gets or sets the KeepFormOpenResult.
        /// </summary>
        /// 
        [DefaultValue(DialogResult.Cancel)]
        [Description("Specifies which button keeps the form opened.")]
        public DialogResult KeepFormOpenResult { get; set; } = DialogResult.Cancel;

        /// <summary>
        /// Gets or sets the Caption.
        /// </summary>
        [DefaultValue("Confirm form closing")]
        [Description("The title of confirmation dialog")]
        public string Caption { get; set; } = "Confirm form closing";

        /// <summary>
        /// Gets or sets the ButtonSet.
        /// </summary>
        /// 
        [DefaultValue(ButtonSetOptions.OKCancel)]
        [Description("Determines which set of buttons appear in confirmation dialog")]
        public ButtonSetOptions ButtonSet { get; set; } = ButtonSetOptions.OKCancel;

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        /// 
        [DefaultValue(MessageBoxIcon.Question)]
        [Description("The icon to show in confirmation dialog")]
        public MessageBoxIcon Icon { get; set; } = MessageBoxIcon.Question;

        /// <summary>
        /// Gets or sets a value indicating whether IsDirty.
        /// </summary>
        [DefaultValue(false)]
        [Description("Indicates whether form content has changed")]
        public bool IsDirty { get; set; }

        /// <summary>
        /// Gets or sets the TextLine1.
        /// </summary>
        /// 
        [DefaultValue("Dialog content changed.")]
        [Description("The first line of text in confirmation dialog for OKCancel button set")]
        public string TextLine1 { get; set; } = "Dialog content changed.";

        /// <summary>
        /// Gets or sets the TextLine2.
        /// </summary>
        /// 
        [DefaultValue("If you close the dialog, dialog content changes will be lost. If you want to close the dialog and lose the changes, click OK. To keep the dialog open, click Cancel.")]
        [Description("The second line of text in confirmation dialog for OKCancel button set")]
        public string TextLine2 { get; set; } = "If you close the dialog, dialog content changes will be lost. If you want to close the dialog and lose the changes, click OK. To keep the dialog open, click Cancel.";

        /// <summary>
        /// Gets or sets the TextLine3.
        /// </summary>
        /// 
        [DefaultValue("Are you sure you want to close the form?")]
        [Description("The third line of text in confirmation dialog for OKCancel button set")]
        public string TextLine3 { get; set; } = "Are you sure you want to close the form?";

        /// <summary>
        /// Gets or sets the TextLine4.
        /// </summary>
        /// 
        [DefaultValue("Form content changed.")]
        [Description("The first line of text in confirmation dialog for YesNoCancel button set")]
        public string TextLine4 { get; set; } = "Form content changed.";

        /// <summary>
        /// Gets or sets the TextLine5.
        /// </summary>
        /// 
        [DefaultValue("If you want to close the form and save its content, click Yes. To close the form, but lose the changes, click No. To keep the form open, click Cancel.")]
        [Description("The second line of text in confirmation dialog for YesNoCancel button set")]
        public string TextLine5 { get; set; } = "If you want to close the form and save its content, click Yes. To close the form, but lose the changes, click No. To keep the form open, click Cancel.";

        /// <summary>
        /// Gets or sets the TextLine6.
        /// </summary>
        /// 
        [DefaultValue("Do you want to close the form and save its content?")]
        [Description("The third line of text in confirmation dialog for YesNoCancel button set")]
        public string TextLine6 { get; set; } = "Do you want to close the form and save its content?";

        /// <summary>
        /// Asks user to confirm closing the form by showing the closing dialog.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="FormClosingEventArgs"/>.</param>
        public void ConfirmFormClosing(object sender, FormClosingEventArgs e)
        {
            Form s = (Form)sender;
            bool shouldConfirmClosing = (s.DialogResult == DialogResult.Cancel) || (s.DialogResult == DialogResult.None);

            if (IsDirty && shouldConfirmClosing)
            {
                MessageBoxButtons buttons = (ButtonSet == ButtonSetOptions.OKCancel) ? MessageBoxButtons.OKCancel : MessageBoxButtons.YesNoCancel;
                var defaultButton = (ButtonSet == ButtonSetOptions.OKCancel) ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button3;

                ConfirmationDialogResult = MessageBox.Show(GetTextLines(), Caption, buttons, Icon, defaultButton);
                e.Cancel = (ConfirmationDialogResult == KeepFormOpenResult);
            }
        }

        /// <summary>
        /// The GetTextLines.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetTextLines()
        {
            string[] lines = new string[3];
            if (ButtonSet == ButtonSetOptions.OKCancel)
            {
                lines[0] = TextLine1;
                lines[1] = TextLine2;
                lines[2] = TextLine3;
            }
            else
            {
                lines[0] = TextLine4;
                lines[1] = TextLine5;
                lines[2] = TextLine6;
            }

            return string.Format(@"{1}{0}{0}{2}{0}{0}{3}", Environment.NewLine, lines[0], lines[1], lines[2]);
        }
    }

    /// <summary>
    /// Defines the ButtonSetOptions.
    /// </summary>
    public enum ButtonSetOptions
    {
        /// <summary>
        /// Defines the OKCancel.
        /// </summary>
        OKCancel = MessageBoxButtons.OKCancel,

        /// <summary>
        /// Defines the YesNoCancel.
        /// </summary>
        YesNoCancel = MessageBoxButtons.YesNoCancel
    }
}
