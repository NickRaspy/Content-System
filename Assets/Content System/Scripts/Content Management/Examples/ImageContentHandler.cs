using System.IO;
using UnityEngine;

namespace URIMP.Examples
{
    public class ImageContentHandler : IContentHandler
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
            byte[] imageData = File.ReadAllBytes(filePath);
            Texture2D texture = new(2, 2);
            texture.LoadImage(imageData);
            return new ImageContent(Path.GetFileNameWithoutExtension(filePath), Path.GetFileName(filePath), texture);
        }

        public void SaveContent(IContent content, string filePath)
        {
            if (content is ImageContent imageContent)
            {
                byte[] imageData = imageContent.Image.EncodeToPNG();
                File.WriteAllBytes(filePath, imageData);
            }
        }

        public void SaveSubcontent(ISubcontent subcontent, string filePath)
        {

        }
    }
}