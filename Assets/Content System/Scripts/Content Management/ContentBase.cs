namespace URIMP
{
    /// <summary>
    /// Абстрактный базовый класс для всех типов контента.
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

        /// <summary>
        /// Преобразует объект контента в объект ContentData.
        /// </summary>
        /// <returns>Возвращает объект ContentData, содержащий информацию о контенте.</returns>
        /// <remarks>Тип контента определяется на основе имени класса.</remarks>
        public ContentData ToContentData()
        {
            return new ContentData
            {
                Id = Id,
                Name = Name,
                Type = GetType().Name // Предполагается, что имя типа соответствует типу в JSON
            };
        }
    }
}