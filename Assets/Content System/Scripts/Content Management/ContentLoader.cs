using System.Collections;
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
        protected T storage;

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
            string[] args = System.Environment.GetCommandLineArgs();

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
        protected IContent LoadContentFromFile(string directoryName)
        {
            string filePath = Path.Combine(ContentManager.Instance.ContentPath, directoryName);
            if (Directory.Exists(filePath) || File.Exists(filePath))
            {
                foreach(var handler in ContentManager.Instance.GetContentHandlers())
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
                        // Ошибка обработки не указана, игнорируется
                    }
                }
            }

            Debug.LogError("Ошибка загрузки контента.");
            return null;
        }

        /// <summary>
        /// Перечисление для определения следующей сцены.
        /// </summary>
        private enum NextScene
        {
            Main, Edit
        }
    }
}