using UnityEngine;
using UnityEngine.UI;

namespace URIMP.Examples
{
    public abstract class PersonScreen : ManipulationScreen
    {
        [SerializeField] private Image personPageView;

        protected BookManipulator bookManipulator;

        protected virtual void OnEnable()
        {
            if (contentManipulator is not BookManipulator)
            {
                Debug.LogError("Wrong manipulator. Book manipulator is required!");
                return;
            }

            bookManipulator = (BookManipulator)contentManipulator;
        }

        public void SetPageView(Sprite image) => personPageView.sprite = image;
    }
}