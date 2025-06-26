using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Абстрактный класс для манипуляции контентом, включая создание, сохранение, удаление и редактирование.
    /// </summary>
    public abstract class ContentManipulator : MonoBehaviour
    {
        protected string contentType;
        private string contentPath;

        /// <summary>
        /// Новый контент, который может быть создан.
        /// </summary>
        /// <value>Объект, представляющий новый контент.</value>
        public IContent NewContent { get; private set; }

        /// <summary>
        /// Инициализация манипулятора контента.
        /// </summary>
        /// <remarks>Должен быть реализован в производных классах для выполнения специфической инициализации.</remarks>
        public abstract void Init();

        private void Start()
        {
            contentPath = ContentManager.Instance.ContentPath;
            Init();
        }

        /// <summary>
        /// Сохраняет контент в файл.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <returns>Возвращает true, если контент успешно сохранен, иначе false.</returns>
        /// <remarks>Метод пытается использовать все зарегистрированные обработчики для сохранения контента.</remarks>
        public bool SaveContentToFile(string id)
        {
            IContent content = ContentManager.Instance.GetContent<IContent>(id);
            string filePath = Path.Combine(contentPath, content.Name);

            try
            {
                ContentManager.Instance.GetContentHandler(contentType).SaveContent(content, filePath);
                return true;
            }
            catch
            {
                // Если обработчик не подходит, продолжаем
            }

            Debug.LogError("Не удалось сохранить контент.");
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
            string filePath = Path.Combine(contentPath, ContentManager.Instance.GetContent<IContent>(id).Name, subcontent.Name);

            try
            {
                ContentManager.Instance.GetContentHandler(contentType).SaveSubcontent(subcontent, filePath);
                return true;
            }
            catch
            {
                // Если обработчик не подходит, продолжаем
            }

            Debug.LogError("Не удалось сохранить подконтент.");
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
            string filePath = Path.Combine(contentPath, ContentManager.Instance.GetContent<IContent>(id).Name);
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
            string filePath = Path.Combine(contentPath, ContentManager.Instance.GetContent<IContent>(id).Name, subcontent.Name);
            return DeleteFile(filePath);
        }

        /// <summary>
        /// Удаляет файл по указанному пути.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Возвращает true, если файл успешно удален, иначе false.</returns>
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
            string filePath = Path.Combine(contentPath, ContentManager.Instance.GetContent<IContent>(id).Name);
            try
            {
                ContentManager.Instance.GetContentHandler(contentType).EditContent(previousContent, newContent, filePath);
            }
            catch
            {
                Debug.LogError("Не удалось изменить контент.");
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
            string filePath = Path.Combine(contentPath, ContentManager.Instance.GetContent<IContent>(id).Name);
            try
            {
                ContentManager.Instance.GetContentHandler(contentType).EditSubcontent(previousSubcontent, newSubcontent, filePath);
            }
            catch
            {
                Debug.LogError("Не удалось изменить контент.");
            }
        }
    }
}