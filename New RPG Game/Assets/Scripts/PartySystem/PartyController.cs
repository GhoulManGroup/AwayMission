using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PartyManagement
{   /// <summary>
    ///         //This class will manage and track the player character and their two maximum companion characters in the party.
    /// </summary>
    public class PartyController : MonoBehaviour
    {

        public PartyInterface partyInterfaceUI = null;
        /// <summary>
        /// This class will manage the games party system for controlling the primary player and their companions in the world when outside of combat
        /// This will set and manage the active party determine if the active party moves individualy or togegther what formation ect
        /// Will be called by partyMember class to check to see if they should be allowed to move on there own or as a unit
        /// </summary>
        /// 
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

        private void Awake()
        {
            while (Manager.instance == null)
            {
                return;
            }

            Manager.instance.partyController = this;

        }



        public void Start()
        {
            //Grab player test characters untill we set up a more concreete system
            currentPartyMembers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        }
    }

}