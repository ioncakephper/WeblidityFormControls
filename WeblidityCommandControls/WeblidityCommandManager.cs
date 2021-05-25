using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeblidityCommandControls
{
    public partial class WeblidityCommandManager : Component
    {
        public Stack<WeblidityCommand> Commands = new Stack<WeblidityCommand>();
        public WeblidityCommandManager()
        {
            InitializeComponent();
        }

        public WeblidityCommandManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Invoke(WeblidityCommand weblidityCommand)
        {
            if (weblidityCommand == null)
            {
                throw new ArgumentNullException(nameof(weblidityCommand));
            }

            Commands.Push(weblidityCommand);
            if (weblidityCommand.CanExecute())
            {
                weblidityCommand.Execute();
            }
        }

        public void Undo()
        {
            if (Commands.Count > 0)
            {
                var c = Commands.Pop();
                c.Undo();
            }
        }
    }
}
