using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace URIMP
{
    public class ContentManager : MonoBehaviour
    {
        private const string ContentDirectory = "Content";
        private const string MetadataFile = "content_metadata.json";

        public string ContentPath => Path.Combine(Application.streamingAssetsPath, ContentDirectory);

        private Dictionary<string, IContent> contentDictionary;

        #region INSTANCE

        public static ContentManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                contentDictionary = new Dictionary<string, IContent>();

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

        #endregion

        #region CONTENT_MANIPULATION

        public void AddContent(IContent content)
        {
            contentDictionary[content.Id] = content;
            SaveMetadata();
        }

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

        public IContent GetContent(string id)
        {
            contentDictionary.TryGetValue(id, out IContent content);
            return content;
        }

        public IEnumerable<IContent> GetAllContent()
        {
            return contentDictionary.Values;
        }

        public IEnumerable<IContent> GetAllContent(string id)
        {
            // Since IDs are unique in a dictionary, this will return a single item or an empty collection
            if (contentDictionary.TryGetValue(id, out IContent content))
            {
                return new List<IContent> { content };
            }
            return new List<IContent>();
        }

        public string GetKey(string value)
        {
            return contentDictionary.FirstOrDefault(pair => pair.Value.Name == value).Key;
        }

        public IEnumerable<string> GetAllKeys()
        {
            return contentDictionary.Keys;
        }

        #endregion

        #region METADATA

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
                contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, IContent>>(json, settings);
            }
        }

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

        #endregion
    }
}