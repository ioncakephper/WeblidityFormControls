using System.Collections.Generic;

namespace WindowsFormsApp3
{
    public class DocumentationProject
    {
        public string Title { get; set; }
        public List<DocumentationSidebar> Sidebars { get; set; } = new List<DocumentationSidebar>();
    }
}