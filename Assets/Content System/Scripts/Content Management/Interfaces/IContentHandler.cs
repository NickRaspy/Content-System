namespace URIMP
{
    /// <summary>
    /// Интерфейс для обработки содержимого, предоставляющий методы для загрузки, сохранения и редактирования.
    /// </summary>
    public interface IContentHandler
    {
        /// <summary>
        /// Загружает содержимое из указанного файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу, из которого будет загружено содержимое.</param>
        /// <returns>Загруженное содержимое.</returns>
        /// <exception cref="IOException">Возникает, если файл не может быть прочитан.</exception>
        IContent LoadContent(string filePath);

        /// <summary>
        /// Сохраняет содержимое в указанный файл.
        /// </summary>
        /// <param name="content">Содержимое, которое нужно сохранить.</param>
        /// <param name="filePath">Путь к файлу, в который будет сохранено содержимое.</param>
        /// <exception cref="IOException">Возникает, если файл не может быть записан.</exception>
        void SaveContent(IContent content, string filePath);

        /// <summary>
        /// Сохраняет под-содержимое в указанный файл.
        /// </summary>
        /// <param name="subcontent">Под-содержимое, которое нужно сохранить.</param>
        /// <param name="filePath">Путь к файлу, в который будет сохранено под-содержимое.</param>
        /// <exception cref="IOException">Возникает, если файл не может быть записан.</exception>
        void SaveSubcontent(ISubcontent subcontent, string filePath);

        /// <summary>
        /// Редактирует содержимое, заменяя старое содержимое новым в указанном файле.
        /// </summary>
        /// <param name="previousContent">Предыдущее содержимое, которое будет заменено.</param>
        /// <param name="newContent">Новое содержимое, которое заменит предыдущее.</param>
        /// <param name="filePath">Путь к файлу, в котором будет произведено редактирование.</param>
        /// <exception cref="IOException">Возникает, если файл не может быть записан.</exception>
        void EditContent(IContent previousContent, IContent newContent, string filePath);

        /// <summary>
        /// Редактирует под-содержимое, заменяя старое под-содержимое новым в указанном файле.
        /// </summary>
        /// <param name="previousSubcontent">Предыдущее под-содержимое, которое будет заменено.</param>
        /// <param name="newSubcontent">Новое под-содержимое, которое заменит предыдущее.</param>
        /// <param name="filePath">Путь к файлу, в котором будет произведено редактирование.</param>
        /// <exception cref="IOException">Возникает, если файл не может быть записан.</exception>
        void EditSubcontent(ISubcontent previousSubcontent, ISubcontent newSubcontent, string filePath);
    }
}