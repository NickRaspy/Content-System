namespace URIMP
{
    /// <summary>
    /// Класс, представляющий данные контента.
    /// </summary>
    public class ContentData
    {
        /// <summary>
        /// Идентификатор контента.
        /// </summary>
        /// <value>Строка, представляющая уникальный идентификатор контента.</value>
        public string Id { get; set; }

        /// <summary>
        /// Имя контента.
        /// </summary>
        /// <value>Строка, представляющая имя контента.</value>
        public string Name { get; set; }

        /// <summary>
        /// Тип контента.
        /// </summary>
        /// <value>Строка, представляющая тип контента.</value>
        public string Type { get; set; }
    }
}