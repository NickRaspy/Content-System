using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace URIMP.Examples
{
    public class BookListController : MonoBehaviour
    {
        [SerializeField] private BookHolder bookHolder;
        [SerializeField] private Transform bookContainer;
        [SerializeField] private BookManipulator bookManipulator;

        public List<Book> Books { get; set; } = new();

        public ManipulationType ManipulationType { get; set; }

        public UnityAction ManipulationAction { get; set; }

        private void OnEnable()
        {
            foreach (IContent content in ContentManager.Instance.GetAllContent())
            {
                if (content is Book book)
                    Books.Add(book);
            }

            LoadList();
        }

        private void OnDisable()
        {
            foreach (Transform child in bookContainer) Destroy(child.gameObject);

            Books.Clear();
        }

        public void LoadList()
        {
            Books.ForEach(b =>
            {
                Instantiate(bookHolder, bookContainer).Init(b, ManipulationAction, bookManipulator);
            });
        }
    }
}