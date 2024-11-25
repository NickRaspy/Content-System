using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URIMP
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;  
        }

        [SerializeField] private GameObject mainScreen;
        [SerializeField] private Button returnButton;

        private GameObject currentScreen;
        void Start()
        {
            currentScreen = mainScreen;

            returnButton.onClick.AddListener(ReturnToMainMenu);
        }

        public void OpenScreen(GameObject screen)
        {
            currentScreen.SetActive(false);

            screen.SetActive(true);

            currentScreen = screen;

            returnButton.gameObject.SetActive(true);
        }

        public void ReturnToMainMenu()
        {
            currentScreen.SetActive(false);

            returnButton.gameObject.SetActive(false);

            mainScreen.SetActive(true);

            currentScreen = mainScreen;
        }
    }
}
