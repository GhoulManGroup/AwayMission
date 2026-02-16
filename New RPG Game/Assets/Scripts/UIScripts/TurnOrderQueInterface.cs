using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderQueInterface : MonoBehaviour
{
    [Header("Icon Display System")]
    [Space(6)]
    public List<GameObject> myIcons = new List<GameObject>();

    public GameObject iconObject;

    public GameObject entityToActList;

    public GameObject turnCounter;

    [Header("Other UI Elements")]
    [Space(6)]
    public Button passActionButton;


    public void Awake()
    {
        while (Manager.instance == null)
        {
            return;
        }

        Manager.instance.turnOrderQueInterface = this;
    }

    public void TurnOrderQueInterfaceState(bool onOrOff)
    {
        if (onOrOff == false)
        {
            this.GetComponent<CanvasGroup>().alpha = 0f;
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha = 1f;
        }
        this.GetComponent<CanvasGroup>().blocksRaycasts = onOrOff;
        this.GetComponent<CanvasGroup>().interactable = onOrOff;
    }

    public void GenerateIcons()
    {
        //Empty any existing icons that could have persisted between combats
        foreach (var item in myIcons)
        {
            RemoveIcon(item);
        }

        //Spawn in the new icons for this combat
        for (int i = 0; i < Manager.instance.entityTracker.activeEntitiesInCombat.Count; i++)
        {
            GameObject newIcon = Instantiate(iconObject, entityToActList.transform);
            myIcons.Add(newIcon);
        }

        UpdateIcons();
    }

    public void UpdateIcons()
    {
        for (int i = 0; i < Manager.instance.entityTracker.activeEntitiesInCombat.Count; i++)
        {
            myIcons[i].GetComponent<CharacterPortrait>().characterController = Manager.instance.entityTracker.activeEntitiesInCombat[i];
            myIcons[i].GetComponent<CharacterPortrait>().SetupPortrait();
        }

        if (Manager.instance.turnController.turnPhase == TurnController.Turnphase.startPhase)
        {
            turnCounter.transform.SetAsLastSibling();
        }

    }

    public void MoveIcon()
    {
        GameObject iconToMove = myIcons[0];
        myIcons.RemoveAt(0);
        myIcons.Add(iconToMove);
        iconToMove.transform.SetAsLastSibling();

    }


    public void RemoveIcon(GameObject purgeThis)
    {
        myIcons.Remove(purgeThis);
        Object.Destroy(purgeThis);
    }
}
