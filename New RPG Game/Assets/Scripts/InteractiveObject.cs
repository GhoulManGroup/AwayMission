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
    }

    public void OnMouseDown()
    {

        if (Input.GetMouseButton(0) && Manager.instance.levelController.levelState == LevelController.LevelState.explore)
        {
            //Start Combat
            //Stop any and all nav mesh movement
            //Assign the player to a tile / the enemey charcter
            //Start combat?
            Manager.instance.levelController.BeginCombat();
        }

        if (Input.GetMouseButtonDown(1))
        {
        }
    }

    public void OnMouseExit()
    {

    }
}
