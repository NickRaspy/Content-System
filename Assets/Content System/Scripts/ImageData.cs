using System;
using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Класс `ImageData` представляет данные изображения, включая путь к файлу и спрайт.
    /// </summary>
    [Serializable]
    public class ImageData
    {
        /// <summary>
        /// Путь к файлу изображения.
        /// </summary>
        /// <value>Строка, представляющая путь к изображению.</value>
        public string Path { get; set; }

        /// <summary>
        /// Спрайт, созданный из изображения.
        /// </summary>
        /// <value>Объект `Sprite`, представляющий изображение.</value>
        public Sprite Image { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса `ImageData`.
        /// </summary>
        /// <param name="path">Путь к файлу изображения.</param>
        /// <param name="image">Спрайт, созданный из изображения.</param>
        public ImageData(string path, Sprite image)
        {
            Path = path;
            Image = image;
        }
    }
}