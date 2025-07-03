using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionInterface : MonoBehaviour
{
    public Character currentCharacter;

    public List<GameObject> actionButtons = new List<GameObject>();
    public List<GameObject> actionPoints = new List<GameObject>();

    public void Awake()
    {
        Manager.instance.actionInterface = this;
    }
    public void SetupActionBar()
    {
       for(int i = 0; i < currentCharacter.myActions.Count; i++)
        {
            actionButtons[i].GetComponent<Button>().interactable = true;
            actionButtons[i].GetComponent<Image>().sprite = currentCharacter.myActions[i].actionIcon;
            actionButtons[i].GetComponent<ActionButton>().myAction = currentCharacter.myActions[i];
        }

        for (int i = 0; i < currentCharacter.actionPoints; i++)
        {
            actionPoints[i].SetActive(true);
        }
    }

    public void ResetActionBar()
    {
        for (int i = 0; i < currentCharacter.myActions.Count; i++)
        {
            actionButtons[i].GetComponent<ActionButton>().interactable = false;
            actionButtons[i].GetComponent<Image>().sprite = null;
            actionButtons[i].GetComponent<ActionButton>().myAction = null;
        }

        for (int i = 0; i < actionPoints.Count; i++)
        {
            actionPoints[i].SetActive(false);
        }

        currentCharacter = null;

    }

    public void ActionBarState(bool onOrOff)
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
}
