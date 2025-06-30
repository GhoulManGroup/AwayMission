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
    }
}
