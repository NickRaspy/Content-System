using System.IO;

namespace URIMP.Examples
{
    public class TextContentHandler : IContentHandler
    {
        public void EditContent(IContent previousContent, IContent newContent, string filePath)
        {
            throw new System.NotImplementedException();
        }

        public void EditSubcontent(ISubcontent previousSubcontent, ISubcontent newSubcontent, string filePath)
        {
            throw new System.NotImplementedException();
        }

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

        public void SaveSubcontent(ISubcontent subcontent, string filePath)
        {

        }
    }
}
