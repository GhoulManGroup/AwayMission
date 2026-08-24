using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CombatSystem
{
    [RequireComponent(typeof(AgentController))]
    public class EntityController : MonoBehaviour
    {

        /// <summary>
        /// This script will manage the details / behaviour of characters in the combat portion of the game 
        /// </summary>
        public Character myCharacter;

        public GameObject currentPosition;

        public GameObject myPortrait;

        [Header("Entity Stats and Attirbutes")]
        //The stats this entity draws from theri character;
        int startingInitative;
        int startingHealth;
        float startingMoveDistance;
        int startingAP;


        public int currentInitative;
        public int currentHealth;
        public float currentMoveDistance;
        public int currentAP;



        public bool hasActed = false;

        public IEnumerator Start()
        {
            while (Manager.instance == null && Manager.instance.entityTracker == null)
            {
                yield return null;
            }

            startingInitative = myCharacter.initiative;
            startingHealth = myCharacter.health;
            startingMoveDistance = myCharacter.moveDistance;
            startingAP = myCharacter.actionPoints;

            currentInitative = startingInitative;
            currentHealth = startingHealth;
            currentMoveDistance = startingMoveDistance;
            currentAP = startingAP;

            Manager.instance.entityTracker.AddEntity(this);
        }

        #region Determine Stat Value
        public int DetermineiInitiative()
        {
            // Will add more code here in future when system fleshedout more to adjust the value based on modifiers.
            float testValue = Random.RandomRange(1f, 10f);

            int convertValue = (int)MathF.Round(testValue);

            startingInitative = myCharacter.initiative + convertValue;

            return myCharacter.initiative + convertValue;
        }
        #endregion

        #region Combat Control Code

        public void OnMouseDown()
        {
            if (Manager.instance.levelController.levelState == LevelController.LevelState.combat)
            {
                Manager.instance.entityTracker.activeEntitiesInCombat[0].DistanceCheck(this.gameObject);
            }
        }

        public void DistanceCheck(GameObject target)
        {
            float betweenUs = Vector3.Distance(this.transform.position, target.transform.position);
            print(betweenUs);
        }

        public bool CheckMovementPointsRemaning()
        {
            if (currentMoveDistance > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public void CheckCondition()
        {
            if (currentHealth <= 0)
            {

            }
        }

        public void CombatRoundOver()
        {
            //reset action 
            currentAP = startingAP;
            currentMoveDistance = startingMoveDistance;
            //Add code later to check for anything tht might subtract from this value like modifiers
        }
    #endregion
    }
}
