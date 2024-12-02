using UnityEngine;
using UnityEngine.UI;

namespace URIMP
{
    /// <summary>
    /// Класс, управляющий элементами пользовательского интерфейса и переходами между экранами в приложении.
    /// Реализует паттерн синглтон для обеспечения единственного экземпляра.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        /// <summary>
        /// Экземпляр синглтона <see cref="UIController"/>.
        /// </summary>
        public static UIController Instance;

        /// <summary>
        /// Основной экран GameObject.
        /// </summary>
        [SerializeField] private GameObject mainScreen;

        /// <summary>
        /// Кнопка, используемая для возврата в главное меню.
        /// </summary>
        [SerializeField] private Button returnButton;

        /// <summary>
        /// Текущий активный экран.
        /// </summary>
        private GameObject currentScreen;

        /// <summary>
        /// Инициализирует экземпляр синглтона и устанавливает начальный экран.
        /// </summary>
        /// <remarks>
        /// Если экземпляр <see cref="UIController"/> уже существует, текущий экземпляр будет уничтожен.
        /// </remarks>
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        /// <summary>
        /// Устанавливает начальный экран и настраивает кнопку возврата.
        /// </summary>
        private void Start()
        {
            currentScreen = mainScreen;
            returnButton.onClick.AddListener(ReturnToMainMenu);
        }

        /// <summary>
        /// Открывает указанный экран и обновляет текущий экран.
        /// </summary>
        /// <param name="screen">Экран, который нужно открыть.</param>
        public void OpenScreen(GameObject screen)
        {
            currentScreen.SetActive(false);
            screen.SetActive(true);
            currentScreen = screen;
            returnButton.gameObject.SetActive(true);
        }

        /// <summary>
        /// Возвращает на экран главного меню.
        /// </summary>
        public void ReturnToMainMenu()
        {
            currentScreen.SetActive(false);
            returnButton.gameObject.SetActive(false);
            mainScreen.SetActive(true);
            currentScreen = mainScreen;
        }
    }
}