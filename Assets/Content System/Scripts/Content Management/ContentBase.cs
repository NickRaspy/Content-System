namespace URIMP
{
    /// <summary>
    /// Абстрактный базовый класс для управления контентом.
    /// </summary>
    public abstract class ContentBase : IContent
    {
        /// <summary>
        /// Идентификатор контента.
        /// </summary>
        /// <value>Уникальный идентификатор контента.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Имя контента.
        /// </summary>
        /// <value>Имя контента, которое может быть изменено.</value>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор для инициализации базового контента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор контента.</param>
        /// <param name="name">Имя контента.</param>
        protected ContentBase(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Загружает контент.
        /// </summary>
        /// <remarks>Метод должен быть реализован в производных классах для загрузки специфического контента.</remarks>
        public abstract void Load();

        /// <summary>
        /// Сохраняет контент.
        /// </summary>
        /// <remarks>Метод должен быть реализован в производных классах для сохранения специфического контента.</remarks>
        public abstract void Save();

        /// <summary>
        /// Удаляет контент.
        /// </summary>
        /// <remarks>Метод должен быть реализован в производных классах для удаления специфического контента.</remarks>
        public abstract void Delete();
    }
}