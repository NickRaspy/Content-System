using System.IO;
using TMPro;
using UnityEngine;

namespace URIMP.Examples
{
    public class PersonDeleteScreen : PersonScreen
    {
        [SerializeField] private TMP_Text personName;

        [SerializeField] private Confirmation confirmationScreen;

        protected override void OnEnable()
        {
            base.OnEnable();

            Person person = bookManipulator.GetPerson();

            personName.text = person.Name;

            SetPageView(ImageMaster.LoadImage(Path.Combine(ContentManager.Instance.ContentPath, person.PersonPagePath)).Image);
        }

        public void Confirmation()
        {
            confirmationScreen.gameObject.SetActive(true);

            confirmationScreen.AddConfirmationAction(() =>
            {
                bookManipulator.DeletePerson();
            });
        }
    }
}