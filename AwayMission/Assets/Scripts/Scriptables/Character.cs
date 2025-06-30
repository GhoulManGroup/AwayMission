using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "AwayMission/ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    [Header("Character Details")]
    int health = 100;
    int movementSpeed = 2;

    string characterName = "Person";


}
