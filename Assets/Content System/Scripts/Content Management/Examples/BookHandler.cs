using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace URIMP.Examples
{
    public class BookHander : IContentHandler
    {
        private const string personPageFileName = "Person_Page.png";
        private const string personNameFileName = "Person_Name.txt";

        public IContent LoadContent(string filePath)
        {
            List<Person> persons = new();
            foreach (string innerPath in Directory.GetDirectories(filePath))
            {
                if (Directory.GetFiles(innerPath).Length > 2)
                {
                    Debug.LogError("Person contains too many files. It must contain only one text file with name and/or one image.");
                    continue;
                }

                Person person = new();

                foreach (string file in Directory.GetFiles(innerPath))
                {
                    switch (Path.GetExtension(file))
                    {
                        case ".png":
                        case ".jpg":
                        case ".jpeg":

                            string[] pathParts = file.Split(Path.DirectorySeparatorChar);

                            person.PersonPagePath = Path.Combine(pathParts[^3], pathParts[^2], pathParts[^1]);

                            break;

                        case ".txt":
                            person.Name = File.ReadAllText(file);
                            break;
                    }
                }

                persons.Add(person);
            }
            Match match = Regex.Match(Path.GetFileName(filePath), @"\d+");
            return new Book($"book_{(match.Success ? Convert.ToInt32(match.Value) : UnityEngine.Random.Range(10000, 99999).ToString())}", Path.GetFileName(filePath), persons);
        }

        public void SaveContent(IContent content, string filePath)
        {
            if (content is Book book)
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var directories = Directory.GetDirectories(filePath);

                for (int i = 0; i < directories.Length; i++)
                {
                    directories[i] = directories[i].Split('\\').Last();
                }

                List<Person> persons = book.Persons;

                persons.RemoveAll(x => directories.Contains(x.Name));

                if (persons.Count > 0)
                    persons.ForEach(p =>
                    {
                        SaveSubcontent(p, Path.Combine(filePath, p.Name));
                    });
            }
        }

        public void SaveSubcontent(ISubcontent subcontent, string filePath)
        {
            if (subcontent is Person person)
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                File.Copy(person.PersonPagePath, Path.Combine(filePath, personPageFileName));

                person.PersonPagePath = Path.Combine(filePath, personPageFileName);

                File.WriteAllText(Path.Combine(filePath, personNameFileName), person.Name);
            }
        }

        public void EditContent(IContent previousContent, IContent newContent, string filePath)
        {
            if (newContent is Book newBook)
            {
                if (Directory.Exists(filePath))
                    Directory.Move(filePath, Path.Combine(ContentManager.Instance.ContentPath, newBook.Name));
            }
        }

        public void EditSubcontent(ISubcontent previousSubcontent, ISubcontent newSubcontent, string filePath)
        {
            if (previousSubcontent is Person previousPerson && newSubcontent is Person newPerson)
            {
                string prevPath = Path.Combine(filePath, previousSubcontent.Name);
                string newPath = Path.Combine(filePath, newPerson.Name);

                if (Directory.Exists(prevPath) && previousPerson.Name != newPerson.Name)
                {
                    Directory.Move(prevPath, newPath);
                    File.Delete(prevPath + ".meta");
                }

                File.Copy(newPerson.PersonPagePath, Path.Combine(newPath, "Person_Page.png"), true);

                newPerson.PersonPagePath = Path.Combine(newPath, "Person_Page.png");
            }
        }
    }
}