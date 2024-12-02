using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace URIMP
{
    /// <summary>
    /// Класс для преобразования объектов, реализующих интерфейс IContent, в JSON и обратно.
    /// </summary>
    public class ContentConverter : JsonConverter
    {
        /// <summary>
        /// Определяет, может ли данный конвертер обрабатывать указанный тип объекта.
        /// </summary>
        /// <param name="objectType">Тип объекта для проверки.</param>
        /// <returns>Возвращает true, если объект реализует интерфейс IContent, иначе false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IContent).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Читает JSON и преобразует его в объект типа IContent.
        /// </summary>
        /// <param name="reader">Читатель JSON.</param>
        /// <param name="objectType">Тип объекта, который требуется создать.</param>
        /// <param name="existingValue">Существующее значение объекта, если имеется.</param>
        /// <param name="serializer">Сериализатор, который вызывает этот метод.</param>
        /// <returns>Возвращает объект типа IContent, созданный из JSON.</returns>
        /// <exception cref="JsonException">Выбрасывается, если JSON не содержит необходимые поля.</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string type = jsonObject["Type"].Value<string>();
            string id = jsonObject["Id"].Value<string>();
            string name = jsonObject["Name"].Value<string>();

            return ContentFactory.CreateContent(new ContentData { Type = type, Id = id, Name = name });
        }

        /// <summary>
        /// Записывает объект типа IContent в JSON.
        /// </summary>
        /// <param name="writer">Писатель JSON.</param>
        /// <param name="value">Объект для записи.</param>
        /// <param name="serializer">Сериализатор, который вызывает этот метод.</param>
        /// <remarks>Метод записывает тип, идентификатор и имя контента в JSON.</remarks>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IContent content = (IContent)value;
            JObject jsonObject = new JObject
            {
                { "Type", content.GetType().Name },
                { "Id", content.Id },
                { "Name", content.Name }
            };
            jsonObject.WriteTo(writer);
        }
    }
}