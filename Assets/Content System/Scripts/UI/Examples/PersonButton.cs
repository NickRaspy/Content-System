using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace URIMP.Examples
{
    public class PersonButton : InfoButton
    {
        private Person person;

        public override void Init(string buttonText, UnityAction buttonAction)
        {
            base.Init(buttonText, buttonAction);
        }
    }
}
