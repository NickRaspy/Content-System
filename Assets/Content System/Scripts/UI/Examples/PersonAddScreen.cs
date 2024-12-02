using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace URIMP.Examples
{
    public class PersonAddScreen : PersonDataScreen
    {
        [SerializeField] private TMP_Dropdown bookList;

        private string selectedBookKey;

        protected override void OnEnable()
        {
            base.OnEnable();

            bookList.ClearOptions();

            List<string> options = new();

            foreach (var content in ContentManager.Instance.GetAllContent())
                options.Add(content.Name);

            print(string.Join(", ", options));

            bookList.AddOptions(options);

            SetKey(bookList.options[0].text);
        }

        public void Add()
        {
            bookManipulator.SelectBook(selectedBookKey);

            bookManipulator.CreatePerson(nameField.text, personPagePath);
        }

        public void OnBookListChange(int index) => SetKey(bookList.options[index].text);

        private void SetKey(string selectedValue)
        {
            selectedBookKey = ContentManager.Instance.GetKey(selectedValue);

            if (selectedBookKey == null)
            {
                Debug.LogError("No value found");
            }
        }
    }
}