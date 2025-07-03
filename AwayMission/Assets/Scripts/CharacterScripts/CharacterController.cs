using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Character myCharacter;
    public GameObject currentPosition;

    public void Start()
    {

    }

    public void OnMouseDown()
    {
        Debug.Log("Pressed Character");
        //Add Action List to This then we will, create a action Ui amanger script to insancate and manage the action Ui Buttons to declare actions 
        //This will lead to actually calling the movement code.
        GameObject.FindGameObjectWithTag("ActionBar").GetComponent<ActionInterface>().currentCharacter = myCharacter;
        GameObject.FindGameObjectWithTag("ActionBar").GetComponent<ActionInterface>().SetupActionBar();
    }

    public void TakeAction()
    {

    }
}
