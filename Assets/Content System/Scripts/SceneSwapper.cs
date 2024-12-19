using UnityEngine;
using UnityEngine.SceneManagement;

namespace URIMP
{
    /// <summary>
    /// Класс `SceneSwapper` отвечает за переключение между двумя сценами в игровом движке Unity.
    /// Он отслеживает нажатие определенной клавиши и переключает активную сцену.
    /// </summary>
    public class SceneSwapper : MonoBehaviour
    {
        /// <summary>
        /// Имя основной сцены, которую нужно загрузить.
        /// </summary>
        [SerializeField] private string mainScene;

        /// <summary>
        /// Имя сцены менеджера, которую нужно загрузить.
        /// </summary>
        [SerializeField] private string managerScene;

        /// <summary>
        /// Клавиша, используемая для переключения сцен.
        /// </summary>
        [SerializeField] private KeyCode sceneSwapButton = KeyCode.F6;

        /// <summary>
        /// Статический экземпляр класса `SceneSwapper` для обеспечения паттерна одиночки.
        /// </summary>
        public static SceneSwapper Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        private void OnGUI()
        {
            if(Event.current.type == EventType.KeyDown && Event.current.keyCode == sceneSwapButton)
            {
                if(SceneManager.GetActiveScene().name == mainScene) 
                    SceneManager.LoadSceneAsync(managerScene);
                else 
                    SceneManager.LoadSceneAsync(mainScene);
            }
        }
    }
}