using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace URIMP.Examples
{
    public class BookHander : IContentHandler
    {
        private readonly BookLoadType bookLoadType;

        public BookHander(BookLoadType bookLoadType) => this.bookLoadType = bookLoadType;
        public IContent LoadContent(string filePath)
        {
            List<Person> persons = new();
            foreach (string innerPath in Directory.GetDirectories(filePath)) 
            {
                if(Directory.GetFiles(innerPath).Length > 2)
                {
                    Debug.LogError("Person contains too many files. It must contain only one text file with name and/or one image.");
                    continue;
                }

                Person person = new();

                foreach(string file in Directory.GetFiles(innerPath))
                {
                    switch (Path.GetExtension(file)) 
                    {
                        case ".png": case ".jpg": case ".jpeg":

                            string[] pathParts = file.Split(Path.DirectorySeparatorChar);

                            person.PersonPagePath = Path.Combine(pathParts[^3], pathParts[^2], pathParts[^1]);

                            break;
                        case ".txt":

                            if(bookLoadType == BookLoadType.Separated)
                                person.Name = File.ReadAllText(file);

                            break;
                    }
                }

                if (bookLoadType == BookLoadType.ByFolderName)
                    person.Name = Path.GetFileName(innerPath);

                persons.Add(person);
            }
            return new Book(Path.GetFileName(filePath), Path.GetFileName(filePath), persons);
        }

        public void SaveContent(IContent content, string filePath)
        {
            if (content is Book book) 
            {
                book.Persons.ForEach(p => 
                {
                    File.Copy(p.PersonPagePath, Path.Combine(filePath, "Person_Page.png"));

                    p.PersonPagePath = Path.Combine(filePath, "Person_Page.png");

                    if (bookLoadType == BookLoadType.Separated)
                        File.WriteAllText(Path.Combine(filePath, "Person_Name.txt"), p.Name);
                });
            }
        }
    }
    public enum BookLoadType
    {
        ByFolderName, Separated
    }
}
