using SFB;
using System.IO;
using UnityEngine;

namespace URIMP
{
    public static class FileSearch
    {
        public static string Search(string extension)
        {
            var path = StandaloneFileBrowser.OpenFilePanel("Открыть файл", "", extension, false);

            return path.Length > 0 ? path[0] : null;
        }

        public static string Search(ExtensionFilter[] extensions)
        {
            var path = StandaloneFileBrowser.OpenFilePanel("Открыть файл", "", extensions, false);

            return path.Length > 0 ? path[0] : null;
        }

        public static ImageData SearchImage()
        {
            string imagePath = Search(new ExtensionFilter[] {new() { Name = "Image", Extensions = new string[] {"png", "jpg", "jpeg"}}});

            if(string.IsNullOrEmpty(imagePath)) return null;

            return ImageMaster.LoadImage(imagePath);
        }
    }
}
