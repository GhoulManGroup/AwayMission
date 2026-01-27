using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PartyManagement
{
    public class PartyController : MonoBehaviour
    {
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
        /// 
        /// </summary>
        public bool freeMovement;

        //This class will manage and track the player character and their companion members.

        public void Start()
        {
            //Grab player test characters untill we set up a more concreete system
            currentPartyMembers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        }
    }

}