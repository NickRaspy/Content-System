using System;
using UnityEngine;

namespace URIMP
{
    [Serializable]
    public class ImageData
    {
        public string Path { get; set; }
        public Sprite Image {  get; set; }
        public ImageData(string path, Sprite image)
        {
            Path = path;
            Image = image;
        }
    }
}
