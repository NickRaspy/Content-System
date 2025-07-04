using System.IO;
using TMPro;
using UnityEngine;

namespace URIMP.Examples
{
    public class PersonEditScreen : PersonDataScreen
    {
        [SerializeField] private TMP_Text bookName;

        protected override void OnEnable()
        {
            base.OnEnable();

            print(bookManipulator == null);

            Person person = bookManipulator.GetPerson();

            bookName.text = bookManipulator.GetBook().Name;

            SetPageView(ImageMaster.LoadImage(Path.Combine(ContentManager.Instance.ContentPath, person.PersonPagePath)));
        }

        public void SetBook(string bookName) => this.bookName.text = bookName;

        public void Edit() => bookManipulator.ChangePerson(nameField.text, personPagePath);
    }
}