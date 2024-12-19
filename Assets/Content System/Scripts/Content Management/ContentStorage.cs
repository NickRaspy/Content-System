using UnityEngine;

namespace URIMP
{
    /// <summary>
    /// Абстрактный класс для хранения и управления контентом.
    /// </summary>
    public abstract class ContentStorage : MonoBehaviour
    {
        #region SINGLETON

        /// <summary>
        /// Экземпляр класса ContentStorage.
        /// </summary>
        /// <value>Синглтон экземпляр для доступа к методам и свойствам класса.</value>
        public static ContentStorage Instance;

        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DefineContentStorage();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Определяет хранилище контента.
        /// </summary>
        /// <remarks>Должен быть реализован в производных классах для определения специфического хранилища контента.</remarks>
        public abstract void DefineContentStorage();

        /// <summary>
        /// Определяет, загружен ли контент.
        /// </summary>
        /// <value>Возвращает true, если контент загружен, иначе false.</value>
        public bool IsLoaded { get; protected set; }

        /// <summary>
        /// Загружает контент.
        /// </summary>
        /// <remarks>Должен быть реализован в производных классах для загрузки специфического контента.</remarks>
        public abstract void LoadContent();
    }
}