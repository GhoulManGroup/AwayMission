using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PartyManagement
{
    public class PartyPortraitUI : MonoBehaviour
    {
        public bool amLead;
        public bool amSelected;

        public Image myIcon;
        public Image amSelectedMarker;

        public Character myCharacter;

        public void SetupPortrait()
        {
            myIcon.sprite = myCharacter.myIcon;

            amSelectedMarker.gameObject.SetActive(amSelected);
        }

        public void UpdatePortrait()
        {
            amSelectedMarker.gameObject.SetActive(amSelected);
        }
    }
}
