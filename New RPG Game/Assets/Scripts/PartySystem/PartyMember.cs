using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PartyManagement
{
    public class PartyMember : MonoBehaviour
    {

        public NavMeshAgent thisAgent;

        public void Start()
        {
            thisAgent = GetComponent<NavMeshAgent>();
            thisAgent.updateRotation = false;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    thisAgent.destination = hit.point;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<NavMeshAgent>().SetDestination(hit.point);
                }
            }
        }
    }
}
