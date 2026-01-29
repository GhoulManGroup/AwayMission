using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CombatSystem
{
    public class EntityController : MonoBehaviour
    {
        /// <summary>
        /// This script will manage the details / behaviour of characters in the combat portion of the game 
        /// </summary>
        public Character myCharacter;
        public GameObject currentPosition;
        public GameObject myPortrait;


        public int myInit;

        public bool hasActed = false;

        public MyOwner myOwner;


        public enum MyOwner
        {
            player, friendly, hostile, neutral
        }


        #region Not Sure if keep Code
        public void OnMouseDown()
        {// For combat the turn contorller should determine and set the active character but perhaps for non combat this should be detemined by on click if we decide to have a party its a unique niche for vtm since no game has a party system
            Debug.Log("Pressed Character");
            if (Manager.instance.levelController.levelState == LevelController.LevelState.combat)
            {
                Manager.instance.actionInterface.currentCharacter = myCharacter;
                Manager.instance.actionInterface.ActionBarState(true);
                Manager.instance.actionInterface.SetupActionBar();
            }
            //Add Action List to This then we will, create a action Ui amanger script to insancate and manage the action Ui Buttons to declare actions 
            //This will lead to actually calling the movement code.

        }
        #endregion

        #region Determine Stat Value
        public int DetermineiInitiative()
        {
            // Will add more code here in future when system fleshedout more to adjust the value based on modifiers.
            float testValue = Random.RandomRange(1f, 10f);
            int convertValue = (int)MathF.Round(testValue);
            myInit = myCharacter.initiative + convertValue;
            return myCharacter.initiative + convertValue;
        }
        #endregion
    }
}
