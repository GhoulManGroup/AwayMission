using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "AwayMission/ScriptableObjects/Action", order = 2)]
public class Action : ScriptableObject
{
    public string actionName;

    public Sprite actionIcon;

    public string actionTooltipText;

    public int actionCost; // The cost of action points required to use an ability

    public List<ActionEffect> actionEffects = new List<ActionEffect>();

    public int actionRangeBase; // The number of spaces that the action target can be apart from the current position of the character performing the action
}
