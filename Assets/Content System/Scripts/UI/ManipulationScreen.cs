using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Абстрактный класс, представляющий экран манипуляции контентом.
    /// </summary>
    public abstract class ManipulationScreen : MonoBehaviour
    {
        /// <summary>
        /// Манипулятор контента, связанный с этим экраном.
        /// </summary>
        /// <value>
        /// Ссылка на <see cref="ContentManipulator"/>, используемый для манипуляции контентом.
        /// </value>
        [SerializeField] protected ContentManipulator contentManipulator;
    }
}