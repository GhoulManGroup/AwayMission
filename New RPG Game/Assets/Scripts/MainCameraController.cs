using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Decided to have a single script to manage the main camera direclty rather than have a bunch of camera code scattered across the project.
/// </summary>
public class MainCameraController : MonoBehaviour
{

    public IEnumerator Start()
    {
        while( Manager.instance == null)
        {
            yield return null;
        }

        Manager.instance.mainCameraController = this;
    }

    /// <summary>
    /// This method will assign the main camera to which ever entity is the current active one both in non formation movement and combat. 
    /// </summary>
    public void SwapCameraParent(GameObject newTarget)
    {
        this.transform.parent = newTarget.transform;
        this.transform.position = newTarget.transform.position;
    }
}
