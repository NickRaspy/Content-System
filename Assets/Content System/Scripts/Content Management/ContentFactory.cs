using System;
using System.Collections.Generic;

namespace URIMP
{
    /// <summary>
    /// Статический класс фабрики для создания контента.
    /// </summary>
    public static class ContentFactory
    {
        /// <summary>
        /// Словарь для хранения функций создания контента по типу.
        /// </summary>
        private static readonly Dictionary<string, Func<string, string, IContent>> contentCreators = new Dictionary<string, Func<string, string, IContent>>();

        /// <summary>
        /// Регистрирует новый тип контента.
        /// </summary>
        /// <typeparam name="T">Тип контента, который должен реализовывать интерфейс IContent.</typeparam>
        /// <param name="typeName">Имя типа контента.</param>
        /// <remarks>Если тип с таким именем уже зарегистрирован, он будет перезаписан.</remarks>
        public static void RegisterContentType<T>(string typeName) where T : IContent
        {
            contentCreators[typeName] = (id, name) => (IContent)Activator.CreateInstance(typeof(T), id, name);
        }

        /// <summary>
        /// Создает контент на основе предоставленных данных.
        /// </summary>
        /// <param name="data">Данные контента, содержащие идентификатор, имя и тип.</param>
        /// <returns>Возвращает экземпляр контента, если тип зарегистрирован.</returns>
        /// <exception cref="InvalidOperationException">Выбрасывается, если тип контента не зарегистрирован.</exception>
        public static IContent CreateContent(ContentData data)
        {
            if (contentCreators.TryGetValue(data.Type, out var creator))
            {
                return creator(data.Id, data.Name);
            }
            throw new InvalidOperationException($"Unknown content type: {data.Type}");
        }
    }
}