using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PartyManagement
{
    public class PartyFormation : MonoBehaviour
    {
        public List<GameObject> formationPositions = new List<GameObject>();

        public IEnumerator Start()
        {

            while (Manager.instance.partyController == null)
            {
                yield return null;
            }

            Manager.instance.partyController.partyFormationController = this.gameObject;

            //Manager.instance.referenceManager.Get("FormationBTN").
        }
        public void MovePartyToFormation()
        {
            Manager.instance.partyController.currentPartyMembers[0].GetComponent<NavMeshAgent>().SetDestination(formationPositions[0].transform.position);
            Manager.instance.partyController.currentPartyMembers[1].GetComponent<NavMeshAgent>().SetDestination(formationPositions[1].transform.position);
            Manager.instance.partyController.currentPartyMembers[2].GetComponent<NavMeshAgent>().SetDestination(formationPositions[2].transform.position);
        }
    }

}

