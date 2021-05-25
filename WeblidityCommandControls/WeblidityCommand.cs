//=============================================
// File Name: WeblidityCommand.cs
// Code Formatted on: 5/21/2021 8:10:42 PM
//
// Author: Ion Gireada 
//=============================================

namespace WeblidityCommandControls
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines the <see cref="WeblidityCommand" />.
    /// </summary>
    public partial class WeblidityCommand : Component
    {
        /// <summary>
        /// Defines the CanExecuteCommand.
        /// </summary>
        [Description("Occurs before executing the command")]
        public event EventHandler<CanExecuteCommandEventArgs> CanExecuteCommand;

        /// <summary>
        /// Defines the Command.
        /// </summary>
        [Description("Occurs when executing the command")]
        public event EventHandler Command;

        /// <summary>
        /// Defines the UndoCommand.
        /// </summary>
        [Description("Occurs when undoing the command")]
        public event EventHandler UndoCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeblidityCommand"/> class.
        /// </summary>
        public WeblidityCommand()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Undo the command.
        /// </summary>
        public void Undo()
        {
            if (UndoCommand != null)
            {
                UndoCommand(this, new EventArgs());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeblidityCommand"/> class.
        /// </summary>
        /// <param name="container">The container<see cref="IContainer"/>.</param>
        public WeblidityCommand(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Check whether the command can be executed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CanExecute()
        {
            CanExecuteCommandEventArgs e = new CanExecuteCommandEventArgs() { Cancel = false };
            if (CanExecuteCommand != null)
            {
                CanExecuteCommand(this, e);
            }

            return !e.Cancel;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        public void Execute()
        {
            if (Command != null)
            {
                Command(this, new EventArgs());
            }
        }
    }

    /// <summary>
    /// Defines the <see cref="CanExecuteCommandEventArgs" />.
    /// </summary>
    public class CanExecuteCommandEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether execution of the command is cancelled....
        /// </summary>
        public bool Cancel { get; internal set; }
    }
}
