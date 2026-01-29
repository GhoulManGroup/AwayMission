using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PartyManagement
{
    public class PartyMember : MonoBehaviour
    {

        public NavMeshAgent thisAgent;

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
    }
}
