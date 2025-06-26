using TMPro;
using UnityEngine;

namespace URIMP.Examples
{
    public class PersonDataScreen : PersonScreen
    {
        [SerializeField] protected TMP_InputField nameField;

        protected string personPagePath;

        public void SearchPage()
        {
            var image = FileSearch.SearchImage();

            if (image.Item1 == null || image.Item2 == null) return;

            personPagePath = image.Item2;

            SetPageView(image.Item1);
        }
    }
}