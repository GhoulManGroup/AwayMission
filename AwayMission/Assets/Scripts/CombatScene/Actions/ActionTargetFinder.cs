using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTargetFinder : MonoBehaviour
{
    //based on the target type / required number of targets the target manager will contact required classes to find and store those targets
    //Once target requirement is met will return those targets to the action effect system so it may apply the intended effect.

    public ActionEffect effectToResolve;
    [Header("Target Storage")]
    public List<GameObject> targetsFound = new List<GameObject>(); // List stores the targets that are found by the script or declared by the player

    public void FindTargets()
    {
        switch (effectToResolve.targetMethod)
        {
            case ActionEffect.TargetMethod.chosen:
                
                break;
            case ActionEffect.TargetMethod.areaOfEffect:

                break;
            case ActionEffect.TargetMethod.random:

                break;
        }
    }

    IEnumerator ChooseTarget()
    {

    }

    IEnumerator TargetAreaOfEffect()
    {
        yield return null;
    }

    IEnumerator FindRandomTarget()
    {
        //check range, find every target within range number of tiles from character and select from amongst those till targetPicked = targetsNeeded.
        yield return null;
    }


}

