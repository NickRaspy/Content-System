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
            // ���������� �������� �����������
        }

        public override void Save()
        {
            // ���������� ���������� �����������
        }

        public override void Delete()
        {
            // ���������� �������� �����������
        }
    }
}