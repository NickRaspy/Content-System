using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace URIMP
{
    public abstract class ContentManipulator : MonoBehaviour
    {
        private string contentPath;
        private Dictionary<string, IContentHandler> contentHandlers;

        public IContent NewContent { get; private set; }

        public abstract void Init();

        private void Start()
        {
            contentPath = ContentManager.Instance.ContentPath;
            contentHandlers = new Dictionary<string, IContentHandler>();

            Init();

            print(contentHandlers.Count);
        }

        public void RegisterContentHandler(string type, IContentHandler handler)
        {
            contentHandlers[type] = handler;
        }

        public bool SaveContentToFile(string id)
        {
            IContent content = ContentManager.Instance.GetContent(id);
            if (content == null)
            {
                Debug.LogError("Content not found.");
                return false;
            }

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

            Debug.LogError("No suitable handler found for saving content.");
            return false;
        }

        public bool DeleteContentFile(string id)
        {
            IContent content = ContentManager.Instance.GetContent(id);
            if (content == null)
            {
                Debug.LogError("Content not found.");
                return false;
            }

            string filePath = Path.Combine(contentPath, content.Name);
            if (Directory.Exists(filePath) || File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

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

            Debug.LogError("No suitable handler found for loading content.");
            return null;
        }

        public abstract void DefineContent();
    }
}