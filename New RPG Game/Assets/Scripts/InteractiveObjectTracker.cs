using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The purpose of this class is to store , track and update the state of anything with an interactive object attached to it which indicates it would be interactable for the player 
/// This should let be track what characters need to be loaded in with a scene where they are to be and what state they should be in.
/// 
/// Logic > Load Scene> Each IO call this to find itself > Assign that game object with the data stored here so it knows if its alive dead opened closed or if there is an item to be added to the world.
/// </summary>
public class InteractiveObjectTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
