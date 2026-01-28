using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using PartyManagement;
using static UnityEditor.Progress;
using CombatSystem;

public class TurnController : MonoBehaviour
{
    public List<GameObject> activeEntities = new List<GameObject>();

    Dictionary<int, GameObject> whatIntToEachEntity = new Dictionary<int, GameObject>();

    public List<int> priority = new List<int>();


    public void Awake()
    {
        while (Manager.instance == null)
        {
            return;
        }

        Manager.instance.turnController = this;

        while (Manager.instance.turnOrderQueInterface == null)
        {
            Debug.Log("Waiting for turn order que interface to not be null");
            return;
        }
        Manager.instance.turnOrderQueInterface.PassActionButton.GetComponent<Button>().onClick.AddListener(PassInitiative);
    }

    #region Setup Turn System
    public void SetupTurnController()
    {
        Debug.Log("SetupTurnController");
        // grab the player party, then perform a check to see if anything around them is close enough to become an active entity
        activeEntities.AddRange(this.GetComponentInChildren<PartyController>().currentPartyMembers);

        //Write Future code to grab all intial non awayteam entities nearby to add to active object list.

        //Do check to see if any nearby entities need to become active from idle

        Manager.instance.turnOrderQueInterface.TurnOrderQueInterfaceState(true);

        StartCoroutine(StartTurn());

    }

    public IEnumerator DetermineTurnOrder()
    {
        for (int i = 0; i < activeEntities.Count; i++)
        {
            int priorityNumber = activeEntities[i].GetComponent<EntityController>().DetermineiInitiative();

            //Duplicate Protection
            if (!priority.Contains(priorityNumber))
            {
                priority.Add(priorityNumber);
            }
            else
            {

                int randomNumber = Random.Range(1, 2);
                Debug.Log("Duplicate Found " + randomNumber);

                if (randomNumber == 1)
                {
                    for (int j = 0; j < priority.Count; j++)
                    {
                        if (priority[j] >= priorityNumber)
                        {
                            priority[j] = priority[j] + 1;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < priority.Count; j++)
                    {
                        if (priority[j] <= priorityNumber)
                        {
                            priority[j] = priority[j] - 1;
                        }
                    }
                }

                priority.Add(priorityNumber);
            }
        }

        for (int i = 0; i < priority.Count; i++)
        {
            whatIntToEachEntity.Add(priority[i], activeEntities[i]);
        }

        priority.Sort();

        activeEntities.Clear();

        for (int i = priority.Count - 1; i > -1; i--)
        {
            activeEntities.Add(whatIntToEachEntity[priority[i]]);
        }

        yield return null;

    }

    #endregion

    #region ManageTurnSystem

    public IEnumerator StartTurn()
    {
        //Determine intiative,
        yield return DetermineTurnOrder();

        //then set up UI display for this turn
        Manager.instance.turnOrderQueInterface.UpdateIcons();

        //Determine whose turn to act

        Manager.instance.actionInterface.currentCharacter = activeEntities[0].gameObject.GetComponent<EntityController>().myCharacter;

    }

    public void PassInitiative()
    {

    }

    public void EndTurn()
    {

    }

    public void CheckState()
    {

    }

    public void ResetTurnOrder()
    {

    }

    #endregion
}

