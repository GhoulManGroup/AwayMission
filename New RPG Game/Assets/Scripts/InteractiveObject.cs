using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public InteractionType myInteraction;

    public bool interactionPossible = true;
    public enum InteractionType
    {
        Character, Item, Container, Door,
    }

    public void OnMouseEnter()
    {
        //Display Informaiton Check
        Debug.Log("On Hover Check");
    }

    public void OnMouseDown()
    {
        Debug.Log("On Click Check");

        if (Input.GetMouseButton(0) && Manager.instance.levelController.levelState == LevelController.LevelState.explore)
        {
            Debug.Log("On Click Left Check");
            //Start Combat
            //Stop any and all nav mesh movement
            //Assign the player to a tile / the enemey charcter
            //Start combat?
            Manager.instance.levelController.BeginCombat();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("On Click Right Check");
        }
    }

    public void OnMouseExit()
    {
        Debug.Log("On Hover Exit Check");
    }
}
