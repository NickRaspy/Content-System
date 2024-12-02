namespace URIMP
{
    /// <summary>
    /// Интерфейс, представляющий под-содержимое с именем.
    /// </summary>
    public interface ISubcontent
    {
        /// <summary>
        /// Имя под-содержимого.
        /// </summary>
        /// <value>
        /// Строка, представляющая имя под-содержимого.
        /// </value>
        string Name { get; set; }
    }
}