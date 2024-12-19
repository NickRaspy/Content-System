using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace URIMP.Examples
{
    public class BookManipulator : ContentManipulator
    {
        [SerializeField] private BookLoadType bookLoadType;

        private Book currentBook;

        private Person currentPerson;

        public override void Init()
        {

        }

        public void CreateBook()
        {
            string lastID = ContentManager.Instance.GetAllContent().Count() > 0 ? ContentManager.Instance.GetAllContent().Where(x => x is Book).Last().Id : "0";

            Match match = Regex.Match(lastID, @"\d+");

            int newNum = Convert.ToInt32(match.Value) + 1;

            string id = "book_" + (match.Success ? newNum : UnityEngine.Random.Range(10000, 99999));

            currentBook = new(id, "", new());
        }

        public void CreatePerson(string name, string imagePath)
        {
            if (currentBook == null) return;

            currentPerson = new() { Name = name, PersonPagePath = imagePath };

            currentBook.Persons.Add(currentPerson);

            SaveSubcontentToFile(currentBook.Id, currentPerson);
        }

        public void SelectBook(string id)
        {
            if (ContentManager.Instance.GetContent(id) is Book book) currentBook = book;
            else Debug.LogError("Данный объект не является книгой");
        }

        public void SelectPerson(Person person) => currentPerson = person;

        public void ChangePerson(string name = null, string imagePath = null)
        {
            Person defaultPerson = new() { Name = currentPerson.Name, PersonPagePath = currentPerson.PersonPagePath };

            if (!string.IsNullOrEmpty(name)) currentPerson.Name = name;
            if (!string.IsNullOrEmpty(imagePath)) currentPerson.PersonPagePath = imagePath;

            EditSubcontent(currentBook.Id, defaultPerson, currentPerson);
        }

        public Person GetPerson()
        { return currentPerson; }

        public Book GetBook()
        { return currentBook; }

        public void DeletePerson()
        {
            currentBook.Persons.Remove(currentPerson);

            DeleteSubcontentFile(currentBook.Id, currentPerson);
        }
    }
}