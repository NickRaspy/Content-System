using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace URIMP
{
    /// <summary>
    /// Абстрактный класс для загрузки контента и управления сценами.
    /// </summary>
    public abstract class ContentLoader<T> : MonoBehaviour where T : ContentStorage<T>
    {
        /// <summary>
        /// Хранилище контента.
        /// </summary>
        protected T storage;

        [SerializeField] private bool mustChangeScene;

        /// <summary>
        /// Следующая сцена для загрузки.
        /// </summary>
        /// <value>Перечисление, определяющее следующую сцену.</value>
        [SerializeField] private NextScene nextScene;

        /// <summary>
        /// Имя основной сцены.
        /// </summary>
        /// <value>Строка, содержащая имя основной сцены.</value>
        [SerializeField] private string mainSceneName;

        /// <summary>
        /// Имя сцены редактирования.
        /// </summary>
        /// <value>Строка, содержащая имя сцены редактирования.</value>
        [SerializeField] private string editSceneName;

        protected virtual List<ContentWithHandler> ContentWithHandlers { get; set; } = new();

        private void Start() => Init();

        /// <summary>
        /// Инициализирует загрузчик контента.
        /// </summary>
        /// <remarks>Вызывается для регистрации обработчика контента, подготовки контента и загрузки сцены.</remarks>
        private void Init()
        {
            RegisterContentHandler();
            PrepareContent();

            storage = ContentStorage<T>.Instance;

            IEnumerator Loading()
            {
                yield return storage.LoadContent();

                OnFinishLoading();

                if (!mustChangeScene) yield break;
#if UNITY_EDITOR
                switch (nextScene)
                {
                    case NextScene.Main:
                        SceneManager.LoadScene(mainSceneName);
                        break;
                    case NextScene.Edit:
                        SceneManager.LoadScene(editSceneName);
                        break;
                }
#else
            string[] args = Environment.GetCommandLineArgs();

            if (args.Contains("-editmode")) 
                SceneManager.LoadScene(editSceneName);
            else 
                SceneManager.LoadScene(mainSceneName);
#endif
            }

            StartCoroutine(Loading());
        }

        /// <summary>
        /// Регистрирует обработчик контента.
        /// </summary>
        /// <remarks>Должен быть реализован в производных классах для регистрации специфического обработчика контента.</remarks>
        protected abstract void RegisterContentHandler();

        /// <summary>
        /// Подготавливает контент.
        /// </summary>
        /// <remarks>Должен быть реализован в производных классах для подготовки специфического контента.</remarks>
        protected abstract void PrepareContent();

        /// <summary>
        /// Загружает контент из файла.
        /// </summary>
        /// <param name="directoryName">Имя директории, содержащей контент.</param>
        /// <param name="type">Тип контента для загрузки.</param>
        /// <returns>Загруженный контент или null, если загрузка не удалась.</returns>
        /// <remarks>Использует ContentManager для загрузки и добавления контента.</remarks>
        protected IContent LoadContentFromFile(string directoryName, string handlerType)
        {
            string filePath = Path.Combine(ContentManager.Instance.ContentPath, directoryName);
            if (Directory.Exists(filePath) || File.Exists(filePath))
            {
                IContentHandler handler = ContentManager.Instance.GetContentHandler(handlerType);
                IContent content = handler.LoadContent(filePath);
                if (content != null)
                {
                    ContentManager.Instance.AddContent(handlerType, content);
                    return content;
                }
                try
                {

                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }

            Debug.LogError("Ошибка загрузки контента.");

            return null;
        }

        protected void MultipleLoad()
        {
            foreach (ContentWithHandler cwh in ContentWithHandlers)
            {
                ContentManager.Instance.RegisterContentHandler(cwh.HandlerName, cwh.ContentHandler);

                string path = Path.Combine(ContentManager.Instance.ContentPath, cwh.ContentDirectory);

                if (Directory.Exists(path) && Directory.GetDirectories(path).Length > 0)
                    if (cwh.ContaintmentType == ContaintmentType.Multiple)
                        foreach (string content in Directory.GetDirectories(path))
                            LoadContentFromFile(content, cwh.HandlerName);
                    else
                        LoadContentFromFile(path, cwh.HandlerName);


                else if (Directory.Exists(path) && Directory.GetFiles(path).Length > 0 && Directory.GetDirectories(path).Length == 0)
                    if (cwh.ContaintmentType == ContaintmentType.Multiple)
                        foreach (string content in Directory.GetFiles(path))
                        {
                            if (Path.GetExtension(content) != ".meta")
                                LoadContentFromFile(content, cwh.HandlerName);
                        }
                    else
                        LoadContentFromFile(Directory.GetFiles(path).FirstOrDefault(x => Path.GetExtension(x) != ".meta"), cwh.HandlerName);
            }
        }

        /// <summary>
        /// Вызывается после завершения загрузки контента.
        /// </summary>
        protected virtual void OnFinishLoading()
        {

        }

        /// <summary>
        /// Перечисление для определения следующей сцены.
        /// </summary>
        private enum NextScene
        {
            Main, Edit
        }

        public class ContentWithHandler
        {
            public string ContentDirectory { get; private set; }
            public string HandlerName { get; private set; }
            public IContentHandler ContentHandler { get; private set; }
            public ContaintmentType ContaintmentType { get; private set; }


            public ContentWithHandler(string contentDirectory, string handlerName, IContentHandler contentHandler, ContaintmentType containtmentType)
            {
                ContentDirectory = contentDirectory;
                HandlerName = handlerName;
                ContentHandler = contentHandler;
                ContaintmentType = containtmentType;
            }
        }

        public enum ContaintmentType
        {
            Singular, Multiple
        }
    }
}