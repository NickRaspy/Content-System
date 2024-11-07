using UnityEngine;
using System.IO;

namespace URIMP.Examples
{
    public class ImageContent : ContentBase
    {
        public Texture2D Image { get; set; }

        public ImageContent(string id, string name, Texture2D image) : base(id, name)
        {
            Image = image;
        }

        public override void Load()
        {
            // Реализация загрузки изображения
        }

        public override void Save()
        {
            // Реализация сохранения изображения
        }

        public override void Delete()
        {
            // Реализация удаления изображения
        }
    }
}