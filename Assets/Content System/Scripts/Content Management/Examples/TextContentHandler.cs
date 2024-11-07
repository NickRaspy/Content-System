using System.IO;

namespace URIMP.Examples
{
    public class TextContentHandler : IContentHandler
    {
        public IContent LoadContent(string filePath)
        {
            string content = File.ReadAllText(filePath);
            return new TextContent(Path.GetFileNameWithoutExtension(filePath), Path.GetFileName(filePath), content);
        }

        public void SaveContent(IContent content, string filePath)
        {
            if (content is TextContent textContent)
            {
                File.WriteAllText(filePath, textContent.Text);
            }
        }
    }
}
