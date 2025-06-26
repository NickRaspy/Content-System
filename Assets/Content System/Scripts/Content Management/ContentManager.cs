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

        /// <summary>
        /// Словарь для хранения контента по идентификатору.
        /// </summary>
        private Dictionary<string, Dictionary<string, IContent>> contentDictionary = new();

        /// <summary>
        /// Словарь для хранения обработчиков контента по типу.
        /// </summary>
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
        /// <returns>Обработчик контента, если найден, иначе null.</returns>
        public IContentHandler GetContentHandler(string type)
        {
            contentHandlers.TryGetValue(type, out var handler);
            return handler;
        }

        /// <summary>
        /// Возвращает все зарегистрированные обработчики контента.
        /// </summary>
        public IEnumerable<IContentHandler> GetContentHandlers()
        {
            return contentHandlers.Values;
        }

        /// <summary>
        /// Добавляет контент в словарь.
        /// </summary>
        /// <param name="type">Тип контента.</param>
        /// <param name="content">Контент для добавления.</param>
        /// <remarks>Если контент с таким же идентификатором уже существует, он будет перезаписан.</remarks>
        public void AddContent(string type, IContent content)
        {
            if (!contentDictionary.ContainsKey(type))
                contentDictionary[type] = new Dictionary<string, IContent>();

            contentDictionary[type][content.Id] = content;
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
            bool removed = false;
            foreach (var typeDict in contentDictionary.Values)
            {
                if (typeDict.Remove(id))
                    removed = true;
            }
            if (removed)
                SaveMetadata();
            return removed;
        }

        /// <summary>
        /// Получает первый найденный контент типа T по id среди всех типов (handler'ов).
        /// </summary>
        /// <param name="id">Идентификатор контента.</param>
        /// <returns>Возвращает контент типа T, если найден, иначе null.</returns>
        public T GetContent<T>(string id) where T : IContent
        {
            foreach (var typeDict in contentDictionary.Values)
            {
                if (typeDict.TryGetValue(id, out IContent content) && content is T tContent)
                    return tContent;
            }
            return default;
        }

        /// <summary>
        /// Получает все контенты типа T из всех типов (handler'ов).
        /// </summary>
        /// <returns>Перечисление всех контентов типа T.</returns>
        public IEnumerable<T> GetAllContent<T>() where T : IContent
        {
            return contentDictionary.Values
                .SelectMany(dict => dict.Values)
                .OfType<T>();
        }

        /// <summary>
        /// Получает первый найденный id по значению контента (Name).
        /// </summary>
        /// <param name="value">Значение контента (Name).</param>
        /// <returns>id если найден, иначе null.</returns>
        public string GetKey(string value)
        {
            foreach (var typePair in contentDictionary)
            {
                foreach (var contentPair in typePair.Value)
                {
                    if (contentPair.Value.Name == value)
                        return contentPair.Key;
                }
            }
            return null;
        }

        /// <summary>
        /// Получает все id всех контентов всех типов.
        /// </summary>
        /// <returns>Перечисление всех id.</returns>
        public IEnumerable<string> GetAllKeys()
        {
            return contentDictionary.Values.SelectMany(dict => dict.Keys);
        }

        /// <summary>
        /// Получает все пары (тип, id) всех контентов.
        /// </summary>
        /// <returns>Перечисление кортежей (тип, id).</returns>
        public IEnumerable<(string type, string id)> GetAllTypeIdPairs()
        {
            foreach (var typePair in contentDictionary)
            {
                foreach (var id in typePair.Value.Keys)
                {
                    yield return (typePair.Key, id);
                }
            }
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
                    var loaded = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, IContent>>>(json, settings);
                    if (loaded != null)
                        contentDictionary = loaded;
                    else
                        contentDictionary = new Dictionary<string, Dictionary<string, IContent>>();
                }
                catch (JsonException ex)
                {
                    Debug.LogError($"Ошибка при загрузке метаданных: {ex.Message}");
                    contentDictionary = new Dictionary<string, Dictionary<string, IContent>>();
                }
            }
            else
            {
                contentDictionary = new Dictionary<string, Dictionary<string, IContent>>();
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