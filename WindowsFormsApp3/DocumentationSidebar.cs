using System.Collections.Generic;

namespace WindowsFormsApp3
{
    public class DocumentationSidebar
    {
        public string Title { get; set; }
        public List<DocumentationTopic> Topics { get; set; } = new List<DocumentationTopic>();
    }
}