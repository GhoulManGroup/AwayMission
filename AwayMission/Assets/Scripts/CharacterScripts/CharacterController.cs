using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterController : MonoBehaviour
{
    public Character myCharacter;
    public GameObject currentPosition;

    public int myInit;

    public bool hasActed = false;

    public MyOwner myOwner;


    public enum MyOwner
    {
        player, friendly, hostile, neutral
    }

    public void Start()
    {

    }

    public void OnMouseDown()
    {
        Debug.Log("Pressed Character");
        //Add Action List to This then we will, create a action Ui amanger script to insancate and manage the action Ui Buttons to declare actions 
        //This will lead to actually calling the movement code.
        Manager.instance.actionInterface.currentCharacter = myCharacter;
        Manager.instance.actionInterface.ActionBarState(true);
        Manager.instance.actionInterface.SetupActionBar();
    }


    #region Determine Stat Value
    public int DetermineiInitiative()
    {
        // Will add more code here in future when system fleshedout more to adjust the value based on modifiers.
        float testValue = Random.RandomRange(1f, 10f);
        int convertValue = (int)MathF.Round(testValue);
        myInit = myCharacter.initiative + convertValue;
        return myCharacter.initiative + convertValue;
    }
    #endregion
}
