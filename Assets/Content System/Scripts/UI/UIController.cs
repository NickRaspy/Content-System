using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URIMP
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private Button returnButton;

        private GameObject currentScreen;
        void Start()
        {
            returnButton.onClick.AddListener(() =>
            {
                currentScreen.SetActive(false);

                returnButton.gameObject.SetActive(false);

                mainScreen.SetActive(true);
            });
        }

        public void OpenScreen(GameObject screen)
        {
            mainScreen.SetActive(false);

            screen.SetActive(true);

            currentScreen = screen;

            returnButton.gameObject.SetActive(true);
        }
    }
}
