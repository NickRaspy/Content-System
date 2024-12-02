using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace URIMP.Examples
{
    public class BookHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Transform personsHolder;
        [SerializeField] private InfoButton infoButton;

        public Book Book { get; set; }
        public void Init(Book book, UnityAction buttonAction, BookManipulator bookManipulator)
        {
            Book = book;

            title.text = Book.Name;

            foreach (Person person in Book.Persons)
            {
                void bookManipAction()
                {
                    bookManipulator.SelectBook(book.Id);

                    bookManipulator.SelectPerson(person);

                    buttonAction();
                }

                Instantiate(infoButton, personsHolder).Init(person.Name, () => { bookManipAction(); });
            }
        }
    }
}
