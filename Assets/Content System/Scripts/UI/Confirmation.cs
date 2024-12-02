using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace URIMP
{
    /// <summary>
    /// Класс, представляющий окно подтверждения с кнопкой для выполнения действия.
    /// </summary>
    public class Confirmation : MonoBehaviour
    {
        /// <summary>
        /// Кнопка, которая запускает действие подтверждения.
        /// </summary>
        [SerializeField] private Button yesButton;

        /// <summary>
        /// Добавляет действие, которое будет выполнено при нажатии на кнопку подтверждения.
        /// </summary>
        /// <param name="action">Действие, которое нужно добавить к событию нажатия кнопки подтверждения.</param>
        /// <remarks>
        /// Убедитесь, что <see cref="yesButton"/> назначена в инспекторе, чтобы избежать <see cref="NullReferenceException"/>.
        /// </remarks>
        public void AddConfirmationAction(UnityAction action)
        {
            if (yesButton == null)
            {
                Debug.LogError("YesButton не назначена.");
            }
            
            yesButton.onClick.AddListener(action);
        }
    }
}