using UnityEngine;
using System.IO;

namespace URIMP.Examples
{
    public class TextContent : ContentBase
    {
        public string Text { get; set; }

        public TextContent(string id, string name, string text) : base(id, name)
        {
            Text = text;
        }

        public override void Load()
        {
            // Реализация загрузки текстового контента
        }

        public override void Save()
        {
            // Реализация сохранения текстового контента
        }

        public override void Delete()
        {
            // Реализация удаления текстового контента
        }
    }
}