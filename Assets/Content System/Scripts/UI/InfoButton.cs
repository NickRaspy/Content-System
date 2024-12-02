using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace URIMP
{
    /// <summary>
    /// Класс, представляющий кнопку с информацией, которая может быть инициализирована с текстом и действием.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class InfoButton : MonoBehaviour
    {
        /// <summary>
        /// Компонент кнопки, прикрепленный к этому GameObject.
        /// </summary>
        protected Button button;

        /// <summary>
        /// Текстовый компонент, используемый для отображения метки кнопки.
        /// </summary>
        /// <value>
        /// Ссылка на компонент <see cref="TMP_Text"/> для отображения текста.
        /// </value>
        [SerializeField] private TMP_Text text;

        /// <summary>
        /// Инициализирует кнопку с указанным текстом и действием.
        /// </summary>
        /// <param name="buttonText">Текст, отображаемый на кнопке.</param>
        /// <param name="buttonAction">Действие, выполняемое при нажатии на кнопку.</param>
        /// <remarks>
        /// Этот метод настраивает текст кнопки и слушатель события нажатия.
        /// </remarks>
        /// <exception cref="MissingComponentException">
        /// Выбрасывается, если компонент <see cref="Button"/> не прикреплен к GameObject.
        /// </exception>
        public virtual void Init(string buttonText, UnityAction buttonAction)
        {
            if (buttonAction == null)
            {
                Debug.LogWarning("Действие кнопки равно null.");
            }

            button = GetComponent<Button>();
            if (button == null)
            {
                throw new MissingComponentException("Компонент Button обязателен.");
            }

            text.text = buttonText;
            button.onClick.AddListener(buttonAction);
        }
    }
}