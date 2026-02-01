using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PartyManagement
{
    public class PartyMember : MonoBehaviour
    {
        public NavMeshAgent thisAgent;

        public Character myCharacter;

        public IEnumerator Start()
        {
            thisAgent = GetComponent<NavMeshAgent>();

            thisAgent.updateRotation = false;

            while (Manager.instance.partyController== null)
            {
                yield return null;
            }

            Manager.instance.partyController.currentPartyMembers.Add(this.gameObject);
        }

        public void OnMouseDown()
        {
            //Might have to add an on mouse down check here later for doing a friendly action outside of combat perhaps?
            //Will depend on the 

            if (Manager.instance.levelController.levelState == LevelController.LevelState.explore && Manager.instance.partyController.freeMovement == true)
            {
                Manager.instance.partyController.chosenMember = this;
                Manager.instance.mainCameraController.SwapCameraParent(this.gameObject);
            }
        }
    }
}
