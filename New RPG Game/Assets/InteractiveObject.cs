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
}
