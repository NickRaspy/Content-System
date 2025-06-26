using SFB;
using System.IO;
using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Статический класс, предоставляющий методы для поиска и загрузки файлов через диалоговые окна.
    /// </summary>
    public static class FileSearch
    {
        /// <summary>
        /// Открывает диалоговое окно для поиска файла с указанным расширением.
        /// </summary>
        /// <param name="extension">Расширение файла для фильтрации поиска.</param>
        /// <returns>
        /// Путь к выбранному файлу или <c>null</c>, если файл не был выбран.
        /// </returns>
        /// <remarks>
        /// Использует StandaloneFileBrowser для открытия диалогового окна выбора файла.
        /// </remarks>
        public static string Search(string extension)
        {
            var path = StandaloneFileBrowser.OpenFilePanel("Select File", "", extension, false);

            return path.Length > 0 ? path[0] : null;
        }

        /// <summary>
        /// Открывает диалоговое окно для поиска файлов с указанными расширениями.
        /// </summary>
        /// <param name="extensions">Массив <see cref="ExtensionFilter"/> для фильтрации поиска.</param>
        /// <returns>
        /// Путь к выбранному файлу или <c>null</c>, если файл не был выбран.
        /// </returns>
        /// <remarks>
        /// Использует StandaloneFileBrowser для открытия диалогового окна выбора файла с несколькими фильтрами расширений.
        /// </remarks>
        public static string Search(ExtensionFilter[] extensions)
        {
            var path = StandaloneFileBrowser.OpenFilePanel("Select File", "", extensions, false);

            return path.Length > 0 ? path[0] : null;
        }

        /// <summary>
        /// Открывает диалоговое окно для поиска файла изображения и загружает его.
        /// </summary>
        /// <returns>
        /// Объект <see cref="ImageData"/>, содержащий загруженное изображение, или <c>null</c>, если изображение не было выбрано.
        /// </returns>
        /// <remarks>
        /// Этот метод фильтрует поиск по файлам изображений с расширениями "png", "jpg" и "jpeg".
        /// </remarks>
        /// <exception cref="FileNotFoundException">
        /// Выбрасывается, если файл изображения не может быть найден или загружен.
        /// </exception>
        public static (Sprite, string) SearchImage()
        {
            string imagePath = Search(new ExtensionFilter[] { new() { Name = "Image", Extensions = new string[] { "png", "jpg", "jpeg" } } });

            if (string.IsNullOrEmpty(imagePath)) return (null, null);

            return (ImageMaster.LoadImage(imagePath), imagePath);
        }
    }
}