using System;
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

            Debug.LogError("No suitable handler found for saving content.");
            return false;
        }

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

            Debug.LogError("No suitable handler found for saving subcontent.");
            return false;
        }

        public bool DeleteContentFile(string id)
        {
            string filePath = Path.Combine(contentPath, GetContent(id).Name);

            return DeleteFile(filePath);
        }
        public bool DeleteSubcontentFile(string id, ISubcontent subcontent)
        {
            string filePath = Path.Combine(contentPath, GetContent(id).Name, subcontent.Name);

            return DeleteFile(filePath);
        }

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
                    catch(Exception ex)
                    {
                        Debug.LogError($"Can't delete this content because: {ex.Message}");
                    }
                }
            }
            if(isDeleted && File.Exists(filePath + ".meta")) File.Delete(filePath + ".meta");
            return isDeleted;
        }

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

        private IContent GetContent(string id)
        {
            IContent content = ContentManager.Instance.GetContent(id);
            if (content == null)
            {
                Debug.LogError("Content not found.");
                return null;
            }
            return content;
        }

        public abstract void DefineContent();
    }
}