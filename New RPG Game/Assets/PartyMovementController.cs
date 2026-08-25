using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace PartyManagement
{
    public class PartyMovementController : MonoBehaviour
    {
        PartyController partyController = null;

        /// <summary>
        /// This toggle is what determines if the player moves the current priority party member of the entire group when issuing a move action
        /// </summary>
        public enum PartyMovementMode
        {
            noMovement,
            formationMovement,
            freeMovement,
            combatMovement,
        }

        public bool canCoverDistance = false;

        public bool requestedMoveComplete = false;

        public PartyMovementMode partyMovement;

        public IEnumerator start()
        {
            while (Manager.instance == null)
            {
                yield return null;
            }

            partyController = Manager.instance.partyController;
            Manager.instance.partyController.partyMovementController = this;
        }
        #region Party Movement System

        public void Update()
        {
            CheckShouldMove();
        }

        public void CheckShouldMove()
        {
            RaycastHit hit;

            //Check if we are allowed to move this way > Controller
            if (Manager.instance.levelController.levelState == LevelController.LevelState.explore)
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    {
                        if (hit.collider.gameObject.layer == 8)
                        {
                            if (partyMovement == PartyMovementMode.freeMovement)
                            {
                                partyController.chosenMember.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                            }
                            else if (partyMovement == PartyMovementMode.formationMovement)
                            {
                                partyController.partyFormationController.transform.position = hit.point;
                                partyController.partyFormationController.GetComponent<PartyFormation>().MovePartyToFormation();
                            }
                        }
                    }
                }
            }

            else if (Manager.instance.levelController.levelState == LevelController.LevelState.combat)
            {
                if (partyMovement == PartyMovementMode.combatMovement)
                {
                    if (Manager.instance.turnController.currentEntity.GetComponent<PreviewPlayerPath>().moveOrderSent == false && Manager.instance.turnController.currentEntity.CheckMovementPointsRemaning())
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                        {
                            if (hit.collider.gameObject.layer == 8 && !EventSystem.current.IsPointerOverGameObject())
                            {
                                float walkDistanceCheck = Vector3.Distance(Manager.instance.turnController.currentEntity.transform.position, hit.point);
                                float distanceRounded = Mathf.RoundToInt(walkDistanceCheck);

                                if (distanceRounded <= Manager.instance.turnController.currentEntity.currentMoveDistance)
                                {
                                    canCoverDistance = true;
                                }
                                else
                                {
                                    canCoverDistance = false;
                                }

                                Manager.instance.turnController.currentEntity.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                                Manager.instance.turnController.currentEntity.GetComponent<NavMeshAgent>().isStopped = true;
                                Manager.instance.turnController.currentEntity.GetComponent<PreviewPlayerPath>().DrawPath();

                                if (Input.GetMouseButtonDown(0) && canCoverDistance == true)
                                {
                                    Manager.instance.turnController.currentEntity.GetComponent<PreviewPlayerPath>().moveOrderSent = true;
                                    Manager.instance.turnController.currentEntity.GetComponent<NavMeshAgent>().isStopped = false;
                                    Manager.instance.turnController.currentEntity.currentMoveDistance -= distanceRounded;
                                }
                            }
                            else
                            {
                                if (Manager.instance.turnController.currentEntity.GetComponent<LineRenderer>().positionCount != 0)
                                {
                                    Manager.instance.turnController.currentEntity.GetComponent<PreviewPlayerPath>().ClearLine();
                                }
                            }
                        }
                    }
                    else if (Manager.instance.turnController.currentEntity.GetComponent<PreviewPlayerPath>().moveOrderSent == true)
                    {
                        StartCoroutine(Manager.instance.turnController.currentEntity.GetComponent<PreviewPlayerPath>().UpdatePath());
                    }
                }
            }
        }

        public void StopMovement()
        {
            for (int i = 0; i < partyController.currentPartyMembers.Count; i++)
            {
                partyController.currentPartyMembers[i].GetComponent<NavMeshAgent>().SetDestination(partyController.currentPartyMembers[i].transform.position);
            }
        }


        /* public bool MouseOverUI()
         { Use inplace of ispointerovergameobject() if that contiunes to error
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
