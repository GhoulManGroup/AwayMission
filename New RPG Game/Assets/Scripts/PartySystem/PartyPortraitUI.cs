using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PartyManagement
{
    public class PartyPortraitUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        //public bool amLead; // Ppssibly use to determine what position in the formation ? perhaps for now player is head always
        public bool amSelected()
        {
            if (Manager.instance.partyController.chosenMember == ThisCharacter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //The Image component that shows the visual reperesntation of my character
        public Image myIcon;

        //The character scriptable game object I get all my information from for the party member i represent.
        public PartyMember ThisCharacter;

        //Visual object to display that the partyMember I represent is the current selected party member.
        public Image amSelectedMarker;

        // This is the parent class that manages the overall party UI the object this class is on is attached to.
        private PartyGUI myController;

        [SerializeField]
        GameObject expandedPortrait;

        public IEnumerator Start()
        {
            while (Manager.instance == null && Manager.instance.partyController == null && Manager.instance.partyController.partyGUI == null)
            {
                yield return null;
            }

            myController = Manager.instance.partyController.partyGUI;

            expandedPortrait.SetActive(false);

            SetupPortrait();

        }

        public void SetupPortrait()
        {
            myIcon.sprite = ThisCharacter.GetComponent<PartyMember>().myCharacter.myIcon;
            amSelectedMarker.gameObject.SetActive(amSelected());
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("The cursor entered the selectable UI element.");
            expandedPortrait.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Manager.instance.levelController.levelState == LevelController.LevelState.explore && Manager.instance.partyController.freeMovement == true)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    Manager.instance.partyController.chosenMember = ThisCharacter;
                    Manager.instance.mainCameraController.SwapCameraParent(ThisCharacter.gameObject);
                    Manager.instance.partyController.partyGUI.UpdateUI();
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("On Exit Click GUI for Portrait");
            expandedPortrait.SetActive(false);
        }
        
        public void SetPositonOneBTN()
        {
            Manager.instance.partyController.SwapListPosition(0, ThisCharacter.gameObject);
        }

        public void SetPositionTwoBTN()
        {
            Manager.instance.partyController.SwapListPosition(1, ThisCharacter.gameObject);
        }

        public void SetPositionThreeBTN()
        {
            Manager.instance.partyController.SwapListPosition(2, ThisCharacter.gameObject);
        }

    }
}
