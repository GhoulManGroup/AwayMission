using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace PartyManagement
{    
     /// <summary>
     /// This class will manage the games party system for controlling the primary player and their companions in the world when outside of combat
     /// This will set and manage the active party determine if the active party moves individualy or togegther what formation ect
     /// </summary>
    public class PartyController : MonoBehaviour
    {
        #region Variables & declerations
        /// <summary>
        /// This is the UI that represented the current active party at all times tracking health, status ect, should display what character is active or party lead
        /// </summary>
        public PartyInterface partyInterfaceUI = null;

        /// <summary>
        /// this list tracks what game objects are instanciated in the world as party members currently
        /// </summary>
        public List<GameObject> currentPartyMembers = new List<GameObject>();

        /// <summary>
        /// How many characters are currently in the party a base of one being the player character and up to two companions for simplicty sake might change later
        /// </summary>
        public PartySize partySize;

        public enum PartySize
        {
            one, two, three,
        }

        /// <summary>
        /// This toggle is what determines if the player moves the current priority party member of the entire group when issuing a move action
        /// </summary>
        public bool freeMovement;

        /// <summary>
        /// This character is who is at the head of the party when moving as a group and the character that others will follow 
        /// </summary>

        public GameObject partyLead = null;

        /// <summary>
        /// This variable is used to track what party member has been specificaly chosen by the user to act on its own rather than as a party, 
        /// /// </summary>
        public PartyMember chosenMember = null;

        /// <summary>
        /// This object is the parent of the formation move cordiantes and will eventually manage what empty game objects each party member should be moving to
        /// </summary>
        public GameObject partyFormationController;

        #endregion

        private IEnumerator Start()
        {
            while (Manager.instance == null)
            {
                yield return null;
            }
            Manager.instance.partyController = this;
        }

        #region Party Movement System

        public void Update()
        {
            //Check if we are allowed to move this way > Controller
            if (Manager.instance.levelController.levelState == LevelController.LevelState.explore)
            {
                if (Input.GetMouseButtonDown(0) &&!EventSystem.current.IsPointerOverGameObject())
                {   
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    { 
                        if (freeMovement == true)
                        {
                            chosenMember.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                        }
                        else
                        {
                            partyFormationController.transform.position = hit.point;
                            partyFormationController.GetComponent<PartyFormation>().MovePartyToFormation();
                        }
                    }
                }
            }
    
        }

       /* public bool MouseOverUI()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // Expose this as a variable in your script so other components can check for it.
            return results.Count(x => x.gameObject.GetComponent<RectTransform>()) > 0;
        }*/
        #endregion
    }

}