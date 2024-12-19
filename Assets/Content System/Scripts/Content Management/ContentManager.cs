using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Класс для управления контентом и его метаданными.
    /// </summary>
    public class ContentManager : MonoBehaviour
    {
        private const string ContentDirectory = "Content";
        private const string MetadataFile = "content_metadata.json";

        /// <summary>
        /// Путь к директории с контентом.
        /// </summary>
        /// <value>Полный путь к директории с контентом.</value>
        public string ContentPath => Path.Combine(Application.streamingAssetsPath, ContentDirectory);

        private Dictionary<string, IContent> contentDictionary = new();

        private Dictionary<string, IContentHandler> contentHandlers = new();

        #region INSTANCE

        /// <summary>
        /// Экземпляр класса ContentManager.
        /// </summary>
        public static ContentManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                if (!Directory.Exists(ContentPath))
                {
                    Directory.CreateDirectory(ContentPath);
                }

                LoadMetadata();
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion INSTANCE

        #region CONTENT_MANIPULATION

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
        /// Получает обработчик контента для указанного типа.
        /// </summary>
        /// <param name="type">Тип контента.</param>
        /// <returns>Обработчик контента для указанного типа.</returns>
        public IContentHandler GetContentHandler(string type)
        {
            return contentHandlers[type];
        }

        /// <summary>
        /// Добавляет контент в словарь.
        /// </summary>
        /// <param name="content">Контент для добавления.</param>
        /// <remarks>Если контент с таким же идентификатором уже существует, он будет перезаписан.</remarks>
        public void AddContent(IContent content)
        {
            contentDictionary[content.Id] = content;
            SaveMetadata();
        }

        /// <summary>
        /// Удаляет контент из словаря по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор контента для удаления.</param>
        /// <returns>Возвращает true, если контент был успешно удален, иначе false.</returns>
        /// <remarks>Если контент с указанным идентификатором не найден, метод вернет false.</remarks>
        public bool RemoveContent(string id)
        {
            if (contentDictionary.ContainsKey(id))
            {
                contentDictionary.Remove(id);
                SaveMetadata();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Получает контент по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <returns>Возвращает контент, если найден, иначе null.</returns>
        public IContent GetContent(string id)
        {
            contentDictionary.TryGetValue(id, out IContent content);
            return content;
        }

        /// <summary>
        /// Получает все контенты.
        /// </summary>
        /// <returns>Возвращает перечисление всех контентов.</returns>
        public IEnumerable<IContent> GetAllContent()
        {
            return contentDictionary.Values;
        }

        /// <summary>
        /// Получает контент по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <returns>Возвращает перечисление с одним элементом контента или пустое перечисление.</returns>
        public IEnumerable<IContent> GetAllContent(string id)
        {
            if (contentDictionary.TryGetValue(id, out IContent content))
            {
                return new List<IContent> { content };
            }
            return new List<IContent>();
        }

        /// <summary>
        /// Получает ключ по значению контента.
        /// </summary>
        /// <param name="value">Значение контента.</param>
        /// <returns>Возвращает ключ, если найден, иначе null.</returns>
        public string GetKey(string value)
        {
            return contentDictionary.FirstOrDefault(pair => pair.Value.Name == value).Key;
        }

        /// <summary>
        /// Получает все ключи контентов.
        /// </summary>
        /// <returns>Возвращает перечисление всех ключей.</returns>
        public IEnumerable<string> GetAllKeys()
        {
            return contentDictionary.Keys;
        }

        #endregion CONTENT_MANIPULATION

        #region METADATA

        /// <summary>
        /// Загружает метаданные из файла.
        /// </summary>
        private void LoadMetadata()
        {
            string path = Path.Combine(ContentPath, MetadataFile);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                try
                {
                    contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, IContent>>(json, settings);
                }
                catch (JsonException ex)
                {
                    Debug.LogError($"Ошибка при загрузке метаданных: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Сохраняет метаданные в файл.
        /// </summary>
        /// <remarks>Метаданные сохраняются в формате JSON с использованием настроек сериализации.</remarks>
        public void SaveMetadata()
        {
            string path = Path.Combine(ContentPath, MetadataFile);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(contentDictionary, Formatting.Indented, settings);
            File.WriteAllText(path, json);
        }

        #endregion METADATA
    }
}