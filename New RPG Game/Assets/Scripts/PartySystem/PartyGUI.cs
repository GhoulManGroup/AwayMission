using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace PartyManagement
{
    /// <summary>
    /// This script manages the UI aspect of the player party, spawning in a portrait for each party member 
    /// </summary>
    public class PartyGUI : MonoBehaviour
    {
        [SerializeField]
        Button formationSwapBTN;

        [SerializeField]
        Button freeMoveToggleBTN;

        [SerializeField]
        GameObject portraitContainer;

        public GameObject portraitPrefabToSpawn;

        private List<PartyPortraitUI> portraitUIs = new List<PartyPortraitUI>();


        public IEnumerator Start()
        {
            while (Manager.instance == null && Manager.instance.partyController != null)
            {
                yield return null;
            }

            Manager.instance.partyController.partyGUI = this;

            freeMoveToggleBTN.onClick.AddListener(ToggleFreeMove);

            formationSwapBTN.onClick.AddListener(SwapFormation);

            StartCoroutine(Setup());
        }

        public IEnumerator Setup()
        {
            while (Manager.instance.partyController.currentPartyMembers.Count < Manager.instance.partyController.expectedPartySize)
            {
                Debug.Log("Awaiting party member to be added to List");
                yield return null;
            }

            foreach (var item in Manager.instance.partyController.currentPartyMembers)
            {
                GameObject temp = Instantiate(portraitPrefabToSpawn, portraitContainer.transform);
                temp.GetComponent<PartyPortraitUI>().ThisCharacter = item.GetComponent<PartyMember>();
                portraitUIs.Add(temp.GetComponent<PartyPortraitUI>());
            }
        }

        public void UpdateUI()
        {
            for (int i = 0; i < Manager.instance.partyController.currentPartyMembers.Count; i++)
            {
                portraitUIs[i].ThisCharacter = Manager.instance.partyController.currentPartyMembers[i].GetComponent<PartyMember>();
                portraitUIs[i].SetupPortrait();
            }
        }

        public void SwapFormation()
        {
            Debug.Log("Nothing Added Here Yet Add Formation Code Later");
        }

        public void ToggleFreeMove()
        {
            Manager.instance.partyController.freeMovement = !Manager.instance.partyController.freeMovement;

            if (Manager.instance.partyController.freeMovement == false)
            {
                Manager.instance.mainCameraController.SwapCameraParent(GameObject.FindGameObjectWithTag("Player"));
                Manager.instance.partyController.partyFormationController.gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
                Manager.instance.partyController.partyFormationController.GetComponent<PartyFormation>().MovePartyToFormation();
            }
            else if (Manager.instance.partyController.freeMovement == true)
            {
                Manager.instance.partyController.StopMovement();
            }
        }

        public void ShowHideUI()
        {
            if (formationSwapBTN.gameObject.activeInHierarchy)
            {
                formationSwapBTN.gameObject.SetActive(false);
                freeMoveToggleBTN.gameObject.SetActive(false);
            }
            else
            {
                formationSwapBTN.gameObject.SetActive(true);
                freeMoveToggleBTN.gameObject.SetActive(true);
            }
        }


    }
}


