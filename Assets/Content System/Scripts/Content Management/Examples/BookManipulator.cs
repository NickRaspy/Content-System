using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace URIMP.Examples
{
    public class BookManipulator : ContentManipulator
    {
        [SerializeField] private BookLoadType bookLoadType;
        public override void Init()
        {
            RegisterContentHandler("book", new BookHander(bookLoadType));

            if (Directory.Exists(ContentManager.Instance.ContentPath) && Directory.GetDirectories(ContentManager.Instance.ContentPath).Length > 0)
                foreach (string book in Directory.GetDirectories(ContentManager.Instance.ContentPath))
                    LoadContentFromFile(book);
        }

        public void LoadBook(string path)
        {

        }

        public void SaveBook()
        {

        }

        public override void DefineContent()
        {

        }
    }
}
