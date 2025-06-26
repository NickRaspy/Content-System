using System.IO;
using System.Linq;
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
        public static Sprite LoadImage(string imagePath, float pixelsPerUnit = 100f)
        {
            byte[] imageData = File.ReadAllBytes(imagePath);

            if (Path.GetExtension(imagePath) == ".tif") Debug.Log(string.Join(", ",imageData));

            Texture2D texture = new(2, 2, TextureFormat.RGBA32, false, false);

            if (texture.LoadImage(imageData))
            {
                Sprite sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    Vector2.one * 0.5f,
                    pixelsPerUnit,
                    0,
                    SpriteMeshType.FullRect
                );

                return sprite;
            }
            else
            {
                Debug.LogError("Не удалось загрузить изображение");
                return null;
            }
        }

        public static Texture2D LoadRawTexture(string imagePath)
        {
            byte[] imageData = File.ReadAllBytes(imagePath);

            if (Path.GetExtension(imagePath) == ".tif") Debug.Log(string.Join(", ", imageData));

            Texture2D texture = new(1, 1, TextureFormat.RGBA32, false, false);

            if (texture.LoadImage(imageData))
            {
                return texture;
            }
            else
            {
                Debug.LogError("Не удалось загрузить изображение");
                return null;
            }
        }
    }
}