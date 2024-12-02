using System.IO;
using UnityEngine;

namespace URIMP
{
    public static class ImageMaster
    {
        public static ImageData LoadImage(string imagePath)
        {
            byte[] imageData = File.ReadAllBytes(imagePath);

            Texture2D imageTexture = new(2, 2);

            if (imageTexture.LoadImage(imageData))
            {
                Sprite image = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f));

                return new(imagePath, image);
            }
            else
            {
                Debug.LogError("Не удалось загрузить изображение");
                return null;
            }
        }
    }
}