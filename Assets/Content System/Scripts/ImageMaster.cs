using System.IO;
using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Статический класс `ImageMaster` предоставляет методы для загрузки изображений из файловой системы.
    /// </summary>
    public static class ImageMaster
    {
        /// <summary>
        /// Загружает изображение из указанного пути и создает объект `ImageData`.
        /// </summary>
        /// <param name="imagePath">Путь к файлу изображения.</param>
        /// <returns>
        /// Объект `ImageData`, содержащий путь к изображению и созданный спрайт.
        /// Возвращает null, если загрузка изображения не удалась.
        /// </returns>
        public static ImageData LoadImage(string imagePath)
        {
            byte[] imageData = File.ReadAllBytes(imagePath);

            Texture2D imageTexture = new Texture2D(2, 2);

            if (imageTexture.LoadImage(imageData))
            {
                Sprite image = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f));

                return new ImageData(imagePath, image);
            }
            else
            {
                Debug.LogError("Не удалось загрузить изображение");
                return null;
            }
        }
    }
}