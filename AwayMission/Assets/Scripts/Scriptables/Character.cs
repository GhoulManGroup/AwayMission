using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "AwayMission/ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    [Header("Character Details")]
    public string characterName = "Person";

    public int health = 100;
    public int actionPoints = 5;


    [Header("Character Stats")]
    int strenght = 1;

    [Header("Character Actions")]
    public List<Action> myActions = new List<Action>();
}
