using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to track every game object within the active scene when it is first entered
/// We will currently be using this to track whom within the current enviroment needs to be included in combat when it starts
/// </summary>

public class EntityTracker : MonoBehaviour
{
    [SerializeField]
    private List<EntityController> entityControllers = new List<EntityController>();

    private List<EntityController> hostileEntitys = new List<EntityController>();

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

    public void GetCombatParticipants()
    {
       // foreach (var item in entityControllers)
       // {
            //if (item.myCharacter.who)
        //}
    }
}
