using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to track and pass all non party member entities in the world
/// This will be used to find all the enemeys in the current space we are in and add them to combat when combat begins. 
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
}
