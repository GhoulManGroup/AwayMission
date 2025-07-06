using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionEffectSystem : MonoBehaviour
{
    //Identify the effect stored within the scriptable passed to the actionn system by checking against conditions
    //Then contact the target finder class to find any / all targets for this effect.
    //Once targets are found will apply the desired effect to those targets then inform the action manager that the effect is resolved

    //Example here for movement will be > manager says I want to do oen action> Call this once> Find target > Self > okay target is self > Contact pathfinding >
    //Pathfinding will then go through its steps and return once movement has been concluded once movement is done > return control to manager 

    public ActionEffect effectToResolve;
    public bool targetsChecked = false;
    ActionTargetFinder targetFinder;

    void Start()
    {
        targetFinder = this.GetComponent<ActionTargetFinder>();
    }

    public IEnumerator PrepareAndResolveEffect()
    {
        targetsChecked = false;
        yield return null;
       // this.GetComponent<ActionTargetFinder>().ac = effectToResolve;
        //this.GetComponent<ActionTargetFinder>().FindTarget();
    }

}

