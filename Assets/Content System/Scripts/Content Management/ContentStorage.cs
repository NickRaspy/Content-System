using System.Collections;
using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Абстрактный класс для хранения и управления контентом.
    /// </summary>
    public abstract class ContentStorage<T> : MonoBehaviour where T : ContentStorage<T>
    {
        #region SINGLETON

        /// <summary>
        /// Экземпляр класса ContentStorage.
        /// </summary>
        /// <value>Синглтон экземпляр для доступа к методам и свойствам класса.</value>
        public static T Instance;

        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Определяет, загружен ли контент.
        /// </summary>
        /// <value>Возвращает true, если контент загружен, иначе false.</value>
        public bool IsLoaded { get; protected set; }

        /// <summary>
        /// Загружает контент.
        /// </summary>
        /// <remarks>Должен быть реализован в производных классах для загрузки специфического контента.</remarks>
        public abstract IEnumerator LoadContent();
    }
}