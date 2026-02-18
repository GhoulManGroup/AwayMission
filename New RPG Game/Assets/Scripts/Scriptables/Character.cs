using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "AwayMission/ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    [Header("Character Details")]
    public string characterName = "Person";

    public Sprite myIcon;

    [Header("Character Stats")]
    public int initiative = 5;

    public int health = 100;

    public int actionPoints = 5;

    public float moveDistance = 8;

    [Header("Character Actions")]
    public List<Action> myActions = new List<Action>();

    [Header("Character Ownershio")]

    public WhatAmI whatAmI;

    public enum WhatAmI
    {
        player, partyMember, NPCC
    }

    [Header("Character Flags and Checks")]
    public bool amHostile;

    public bool amAlive()
    {
        if (health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
