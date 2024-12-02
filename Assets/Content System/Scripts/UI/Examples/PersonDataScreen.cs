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
            ImageData image = FileSearch.SearchImage();

            if (image == null) return;

            personPagePath = image.Path;

            SetPageView(image.Image);
        }
    }
}