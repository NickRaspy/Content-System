using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace URIMP
{
    [RequireComponent(typeof(Button))]
    public class InfoButton : MonoBehaviour
    {
        protected Button button;

        [SerializeField] private TMP_Text text;

        public virtual void Init(string buttonText, UnityAction buttonAction)
        {
            Debug.Log(buttonAction == null);

            button = GetComponent<Button>();

            text.text = buttonText;

            button.onClick.AddListener(buttonAction);
        }
    }
}
