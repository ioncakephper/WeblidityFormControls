using System.Collections.Generic;

namespace WindowsFormsApp3
{
    public class DocumentationTopic
    {
        public string Title { get; internal set; }
        public List<DocumentationTopic> Topics { get; set; } = new List<DocumentationTopic>();
    }
}