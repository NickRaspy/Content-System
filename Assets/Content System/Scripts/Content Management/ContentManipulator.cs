using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Абстрактный класс для манипуляции контентом.
    /// </summary>
    public abstract class ContentManipulator : MonoBehaviour
    {
        private string contentPath;
        private Dictionary<string, IContentHandler> contentHandlers;

        /// <summary>
        /// Новый контент, который может быть создан.
        /// </summary>
        public IContent NewContent { get; private set; }

        /// <summary>
        /// Инициализация манипулятора контента.
        /// </summary>
        public abstract void Init();

        private void Start()
        {
            contentPath = ContentManager.Instance.ContentPath;
            contentHandlers = new Dictionary<string, IContentHandler>();

            Init();

            print(contentHandlers.Count);
        }

        /// <summary>
        /// Регистрирует обработчик контента для указанного типа.
        /// </summary>
        /// <param name="type">Тип контента.</param>
        /// <param name="handler">Обработчик контента.</param>
        /// <remarks>Если обработчик для данного типа уже существует, он будет перезаписан.</remarks>
        public void RegisterContentHandler(string type, IContentHandler handler)
        {
            contentHandlers[type] = handler;
        }

        /// <summary>
        /// Сохраняет контент в файл.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <returns>Возвращает true, если контент успешно сохранен, иначе false.</returns>
        /// <remarks>Метод пытается использовать все зарегистрированные обработчики для сохранения контента.</remarks>
        public bool SaveContentToFile(string id)
        {
            IContent content = GetContent(id);

            string filePath = Path.Combine(contentPath, content.Name);
            foreach (var handler in contentHandlers.Values)
            {
                try
                {
                    handler.SaveContent(content, filePath);
                    return true;
                }
                catch
                {
                    // Если обработчик не подходит, продолжаем
                }
            }

            Debug.LogError("Подходящий обработчик для сохранения контента не найден.");
            return false;
        }

        /// <summary>
        /// Сохраняет подконтент в файл.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <param name="subcontent">Подконтент для сохранения.</param>
        /// <returns>Возвращает true, если подконтент успешно сохранен, иначе false.</returns>
        /// <remarks>Метод пытается использовать все зарегистрированные обработчики для сохранения подконтента.</remarks>
        public bool SaveSubcontentToFile(string id, ISubcontent subcontent)
        {
            string filePath = Path.Combine(contentPath, GetContent(id).Name, subcontent.Name);
            foreach (var handler in contentHandlers.Values)
            {
                try
                {
                    handler.SaveSubcontent(subcontent, filePath);
                    return true;
                }
                catch
                {
                    // Если обработчик не подходит, продолжаем
                }
            }

            Debug.LogError("Подходящий обработчик для сохранения подконтента не найден.");
            return false;
        }

        /// <summary>
        /// Удаляет файл контента.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <returns>Возвращает true, если файл успешно удален, иначе false.</returns>
        /// <remarks>Метод удаляет файл контента и его метаданные, если они существуют.</remarks>
        public bool DeleteContentFile(string id)
        {
            string filePath = Path.Combine(contentPath, GetContent(id).Name);

            return DeleteFile(filePath);
        }

        /// <summary>
        /// Удаляет файл подконтента.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <param name="subcontent">Подконтент для удаления.</param>
        /// <returns>Возвращает true, если файл успешно удален, иначе false.</returns>
        /// <remarks>Метод удаляет файл подконтента и его метаданные, если они существуют.</remarks>
        public bool DeleteSubcontentFile(string id, ISubcontent subcontent)
        {
            string filePath = Path.Combine(contentPath, GetContent(id).Name, subcontent.Name);

            return DeleteFile(filePath);
        }

        /// <summary>
        /// Удаляет файл по указанному пути.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Возвращает true, если файл успешно удален, иначе false.</returns>
        /// <exception cref="IOException">Выбрасывается, если файл не может быть удален.</exception>
        private bool DeleteFile(string filePath)
        {
            bool isDeleted = false;
            if (Directory.Exists(filePath) || File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    isDeleted = true;
                }
                catch
                {
                    try
                    {
                        Directory.Delete(filePath, true);
                        isDeleted = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Не удалось удалить контент из-за: {ex.Message}");
                    }
                }
            }
            if (isDeleted && File.Exists(filePath + ".meta")) File.Delete(filePath + ".meta");
            return isDeleted;
        }

        /// <summary>
        /// Редактирует контент.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <param name="previousContent">Предыдущий контент.</param>
        /// <param name="newContent">Новый контент.</param>
        /// <remarks>Метод пытается использовать все зарегистрированные обработчики для редактирования контента.</remarks>
        public void EditContent(string id, IContent previousContent, IContent newContent)
        {
            string filePath = Path.Combine(contentPath, GetContent(id).Name);
            foreach (var handler in contentHandlers.Values)
            {
                try
                {
                    handler.EditContent(previousContent, newContent, filePath);
                }
                catch
                {
                    // Если обработчик не подходит, продолжаем
                }
            }
        }

        /// <summary>
        /// Редактирует подконтент.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <param name="previousSubcontent">Предыдущий подконтент.</param>
        /// <param name="newSubcontent">Новый подконтент.</param>
        /// <remarks>Метод пытается использовать все зарегистрированные обработчики для редактирования подконтента.</remarks>
        public void EditSubcontent(string id, ISubcontent previousSubcontent, ISubcontent newSubcontent)
        {
            string filePath = Path.Combine(contentPath, GetContent(id).Name);
            foreach (var handler in contentHandlers.Values)
            {
                try
                {
                    handler.EditSubcontent(previousSubcontent, newSubcontent, filePath);
                }
                catch
                {
                    // Если обработчик не подходит, продолжаем
                }
            }
        }

        /// <summary>
        /// Загружает контент из файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Возвращает загруженный контент или null, если загрузка не удалась.</returns>
        /// <remarks>Метод пытается использовать все зарегистрированные обработчики для загрузки контента.</remarks>
        public IContent LoadContentFromFile(string fileName)
        {
            string filePath = Path.Combine(contentPath, fileName);
            if (Directory.Exists(filePath) || File.Exists(filePath))
            {
                foreach (var handler in contentHandlers.Values)
                {
                    try
                    {
                        IContent content = handler.LoadContent(filePath);
                        if (content != null)
                        {
                            ContentManager.Instance.AddContent(content);
                            return content;
                        }
                    }
                    catch
                    {
                        // Если обработчик не подходит, продолжаем
                    }
                }
            }

            Debug.LogError("Подходящий обработчик для загрузки контента не найден.");
            return null;
        }

        /// <summary>
        /// Получает контент по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <returns>Возвращает контент, если найден, иначе null.</returns>
        /// <remarks>Метод ищет контент в менеджере контента по указанному идентификатору.</remarks>
        private IContent GetContent(string id)
        {
            IContent content = ContentManager.Instance.GetContent(id);
            if (content == null)
            {
                Debug.LogError("Контент не найден.");
                return null;
            }
            return content;
        }

        /// <summary>
        /// Определяет контент.
        /// </summary>
        public abstract void DefineContent();
    }
}