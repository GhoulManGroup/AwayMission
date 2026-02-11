using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to track every character game object within the active scene when it is first entered
/// We will currently be using this to track whom within the current enviroment needs to be included in combat when it starts
/// </summary>

public class EntityTracker : MonoBehaviour
{
    /// <summary>
    /// All current entitycontrollers belonging to game objects that are present within the current scene / loaded enviroment added at start
    /// </summary>
    [SerializeField]
    private List<EntityController> entityControllers = new List<EntityController>();

    #region Combat System
    /// <summary>
    /// What entities are currently involved in the combat occuring at this moement in time based on either being flagged and friendly player or hostile
    /// </summary>
    public List<EntityController> activeEntitiesInCombat = new List<EntityController>();

    /// <summary>
    /// the initative stat value of each entity involved in the combat to determine the order in which each entity acts.
    /// </summary>

    public List<EntityController> entityToAct = new List<EntityController>();

    public List<EntityController> entityHasActed = new List<EntityController>();

    #endregion
    private IEnumerator Start()
    {
        while (Manager.instance == null)
        {
            yield return null;
        }

        Manager.instance.entityTracker = this;
    }

    public void AddEntity(EntityController controller)
    {
        if (entityControllers.Contains(controller))
        {
            //Do nothing already tracking
        }
        else
        {
            entityControllers.Add(controller);
        }
    }

    public void RemoveEntity(EntityController controller)
    {
        {
            if (entityControllers.Contains(controller))
            {
                entityControllers.Remove(controller);
            }
            else
            {
                //Do Nothing;
            }
        }
    }

    public void GetNPCCombatParticipants()
    {
        foreach (var item in entityControllers)
        {
            if (item.myCharacter.whatAmI == Character.WhatAmI.NPCC)
            {
                if (item.myCharacter.amAlive() == true)
                {
                    if (item.myCharacter.amHostile == true)
                    {
                        activeEntitiesInCombat.Add(item);
                    }
                }
            }

            if (item.myCharacter.whatAmI == Character.WhatAmI.player || item.myCharacter.whatAmI == Character.WhatAmI.partyMember)
            {
                activeEntitiesInCombat.Add(item);
            }
        }
    }
}
