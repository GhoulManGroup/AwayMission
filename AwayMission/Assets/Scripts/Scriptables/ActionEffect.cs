using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "AwayMission/ScriptableObjects/ActionEffect", order = 3)]
public class ActionEffect : ScriptableObject
{
    [Header("Effect Targets")]
    public AllowedTargets allowedTargets;
    public TargetMethod targetMethod;
    //Target Distance will be determined by character stat sheet.

    [Header("Effect Peramiters")]
    public EffectType effectType;

    public ModifiedStat modifiedStat;

    public StatusApplied statusApplied;

    //The base value used for actions before adjustments for stat modifieres or the duration of an effect.
    public int modifiedStatValue;

    public enum AllowedTargets
    {
        self, friendly, neutral, hostile, all, NA
    }

    public enum TargetMethod
    {
        chosen, areaOfEffect, random
    }

    public enum EffectType
    {
        move, statChange, statusEffect, NA
    }

    // this enum determines if the effect changes a characters, immidate values like life or action points, or improves the characters rpg stat values like strenght.
    public enum ModifiedStat
    {
        health, shield, attribute, skill, actionPoint, NA
    }

    public enum StatusApplied
    {
        stunned, busy, NA
    }
}
