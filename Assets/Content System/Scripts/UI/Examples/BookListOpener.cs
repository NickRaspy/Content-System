using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URIMP.Examples
{
    public class BookListOpener : MonoBehaviour
    {
        public ManipulationType manipulationType;

        [SerializeField] private ManipulationScreen requiredScreen;

        public void SetList(BookListController bookListController)
        {
            bookListController.ManipulationType = manipulationType;

            bookListController.ManipulationAction += () => 
            {
                UIController.Instance.OpenScreen(requiredScreen.gameObject);
            };
        }
    }
}
