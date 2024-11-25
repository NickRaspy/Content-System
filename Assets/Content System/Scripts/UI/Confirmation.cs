using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Confirmation : MonoBehaviour
{
    [SerializeField] private Button yesButton;

    public void AddConfirmationAction(UnityAction action) => yesButton.onClick.AddListener(action);
}
