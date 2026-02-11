using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        [Header("Entity Stats and Attirbutes")]

        public int startingInitative;
        public int currentInitative;

        public int startingHealth;
        public int currentHealth;

        public bool hasActed = false;

        public IEnumerator Start()
        {
            while (Manager.instance == null && Manager.instance.entityTracker == null)
            {
                yield return null;
            }

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
        public IEnumerator MoveAction()
        {
            yield return null;
        }

        public void DistanceCheck(GameObject target)
        {
            float betweenUs = Vector3.Distance(this.transform.position, target.transform.position);
            print(betweenUs);
        }


    #endregion
}
}
