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
            // ���������� �������� ���������� ��������
        }

        public override void Save()
        {
            // ���������� ���������� ���������� ��������
        }

        public override void Delete()
        {
            // ���������� �������� ���������� ��������
        }
    }
}