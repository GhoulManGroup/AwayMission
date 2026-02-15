using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using PartyManagement;
using static UnityEditor.Progress;
using CombatSystem;
using UnityEngine.TextCore.Text;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;

public class TurnController : MonoBehaviour
{
    public EntityTracker combatEntitys;

    public EntityController currentEntity;

    public int turnCounter;
    
    IEnumerator Start()
    {
        while (Manager.instance == null)
        {
           yield return null;
        }

        Manager.instance.turnController = this;

        while (Manager.instance.turnOrderQueInterface == null)
        {
            Debug.Log("Waiting for turn order que interface to not be null");
            yield return null;
        }

        Manager.instance.turnOrderQueInterface.passActionButton.GetComponent<Button>().onClick.AddListener(PassInitiative);

        while (Manager.instance.entityTracker == null)
        {
            Debug.Log("Waiting for entiy tracker que interface to not be null");
            yield return null;
        }

        combatEntitys = Manager.instance.entityTracker;
    }

    #region Setup Turn System
    public void SetupTurnController()
    {
        Debug.Log("SetupTurnController");

        //find who is in the current combat
        Manager.instance.entityTracker.GetNPCCombatParticipants();

        //Hide non required UI elements in the party UI
        Manager.instance.partyController.partyGUI.ShowHideUI();

        //Turn on the interface for the combat turn system display
        Manager.instance.turnOrderQueInterface.TurnOrderQueInterfaceState(true);

        turnCounter = 1;

        //begin the combat system
        StartCoroutine(StartTurn());

    }
    /// <summary>
    /// This method gives each entity involved in combat a turn sequence assingment for this round of combat with duplicate protection that bumps values up and down untill
    /// every entity has its own unique value bumping previous values up or down one to ensure that they should still be around what ever object had the same intiative value
    /// 
    /// we declare a temp list called priority to store each entitys intiative value then populate that list with each object in combatEntitys initative values before using a
    /// temp dictionary to store what intiative int value belongs to what game object so we can readd them to the list we got them from after sorting them in order who who goes first
    /// </summary>
    /// <returns></returns>
    /// 
    public IEnumerator DetermineTurnOrder()
    {

        List<int> priority = new List<int>();

        Dictionary<int, EntityController> whatIntToEachEntity = new Dictionary<int, EntityController>();

        if (combatEntitys == null)
        {
            Debug.Log("Is null Why");
            combatEntitys = Manager.instance.entityTracker;
        }

        for (int i = 0; i < combatEntitys.activeEntitiesInCombat.Count; i++)
        {
            int priorityNumber = combatEntitys.activeEntitiesInCombat[i].DetermineiInitiative();

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
            whatIntToEachEntity.Add(priority[i], combatEntitys.activeEntitiesInCombat[i]);
        }

        priority.Sort((higher, lower) => lower.CompareTo(higher));

        combatEntitys.activeEntitiesInCombat.Clear();

        foreach (var item in priority)
        {
            combatEntitys.activeEntitiesInCombat.Add(whatIntToEachEntity[item]);
            whatIntToEachEntity[item].GetComponent<EntityController>().currentInitative = item;
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
        Manager.instance.turnOrderQueInterface.GenerateIcons();

        EntityAct();
    }

    /// <summary>
    /// Check combat list then action based on if player or NPC needs to act 
    /// </summary>
    public void EntityAct()
    {
        currentEntity = combatEntitys.activeEntitiesInCombat[0];

        if (currentEntity.myCharacter.whatAmI == Character.WhatAmI.player || currentEntity.myCharacter.whatAmI == Character.WhatAmI.partyMember)
        {
            //Manager.instance.actionInterface.ActionBarState(true);

            //Manager.instance.actionInterface.SetupActionBar();
        }

        else if (currentEntity.myCharacter.whatAmI == Character.WhatAmI.NPCC)
        {
            Debug.Log("Passing Turn AM AI");

            if (currentEntity.myCharacter.amHostile == true)
            {
                Debug.Log("Passing Turn AM AI");

                PassInitiative();
            }
        }

    }

    /// <summary>
    /// This will be called after every turn / every entity action has occured to see if the combat should contiune or if either side has been wiped out.
    /// </summary>
    public void CheckCombatShouldEnd()
    {

    }

    /// <summary>
    /// Pass Initative 
    /// </summary>
    /// 
    public void PassInitiative()
    {
        Debug.Log("Passed Initiative" + currentEntity.name);

        currentEntity.hasActed = true;

        combatEntitys.activeEntitiesInCombat.Remove(currentEntity);

        combatEntitys.activeEntitiesInCombat.Add(currentEntity);

        Manager.instance.turnOrderQueInterface.UpdateIcons();

        CheckTurnOver();
    }

    /// <summary>
    /// Check if all creatures in activeEntitesInCombat have acted and if so 
    /// </summary>
    public void CheckTurnOver()
    {
        for (int i = 0; i < combatEntitys.activeEntitiesInCombat.Count; i++)
        {
            if (combatEntitys.activeEntitiesInCombat[i].GetComponent<EntityController>().hasActed == false)
            {
                EntityAct();
                break;
            }
            else
            {
                EndTurn();
            }
        } 
    }


    /// <summary>
    /// The end turn occurs when every entity has acted in the que and would be where end of turn effects occur & the end combat check would happen
    /// </summary>
    public void EndTurn()
    {
        Debug.Log("Turn Ending");
        turnCounter++;
        StartCoroutine(StartTurn());
    }

    #endregion
}

