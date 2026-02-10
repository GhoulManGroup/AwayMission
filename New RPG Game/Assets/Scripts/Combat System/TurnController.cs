using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using PartyManagement;
using static UnityEditor.Progress;
using CombatSystem;
using UnityEngine.TextCore.Text;

public class TurnController : MonoBehaviour
{
    public EntityTracker combatEntitys;

    /// <summary>
    /// List of who gets to act first based on the int of each controller in the combat
    /// </summary>
    /// 
    public List<int> priority = new List<int>();
    
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

        Manager.instance.entityTracker.GetNPCCombatParticipants();
        //Add combat perticipant to grid
        Manager.instance.partyController.partyGUI.ShowHideUI();

        Manager.instance.turnOrderQueInterface.TurnOrderQueInterfaceState(true);

        StartCoroutine(StartTurn());

    }
    /// <summary>
    /// This method gives each entity involved in combat a turn sequence assingment for this round of combat with duplicate protection that bumps values up and down untill
    /// every entity has its own unique value bumping previous values up or down one to ensure that they should still be around what ever object had the same intiative value
    /// </summary>
    /// <returns></returns>
    public IEnumerator DetermineTurnOrder()
    {

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
            combatEntitys.whatIntToEachEntity.Add(priority[i], combatEntitys.activeEntitiesInCombat[i]);
        }

        priority.Sort();

        combatEntitys.activeEntitiesInCombat.Clear();

        for (int i = priority.Count - 1; i > -1; i--)
        {
            combatEntitys.activeEntitiesInCombat.Add(combatEntitys.whatIntToEachEntity[priority[i]]);
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

        Manager.instance.actionInterface.currentCharacter = combatEntitys.activeEntitiesInCombat[0].myCharacter;

        //Manager.instance.actionInterface.ActionBarState(true);

        //Manager.instance.actionInterface.SetupActionBar();
    }

    /// <summary>
    /// This method is to declare you wish to stop acting with this entity and allow the next entity in the que to act
    /// Will also need to check if there are other hostile entitys in the que.
    /// </summary>
    public void PassInitiative()
    {
        if ()
    }

    /// <summary>
    /// The end turn occurs when every entity has acted in the que and would be where end of turn effects occur & the end combat check would happen
    /// </summary>
    public void EndTurn()
    {

    }

    /// <summary>
    /// Check every entity has acted 
    /// </summary>
    public void CheckState()
    {
        for (int i = 0; i < Manager.instance.entityTracker.activeEntitiesInCombat.Count; i++)
        {
            
        }
    }

    public bool CombatIsOver()
    {
        foreach (var item in collection)
        {

        }
    }

    public void ResetTurnOrder()
    {

    }

    #endregion
}

