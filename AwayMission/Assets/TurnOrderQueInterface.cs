using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderQueInterface : MonoBehaviour
{
    [Header("Icon Display System")]
    [Space(6)]
    public List<GameObject> myIcons = new List<GameObject>();
    public GameObject iconObject;
    public GameObject iconBar;

    [Header("Other UI Elements")]
    [Space(6)]
    public Button PassActionButton;


    public void Awake()
    {
        while (Manager.instance == null)
        {
            return;
        }

        Manager.instance.turnOrderQueInterface = this;
    }

    public void SetupInterface()
    {
        for (int i = 0; i < Manager.instance.turnController.activeEntities.Count; i++)
        {
            GameObject newIcon = Instantiate(iconObject, iconBar.transform);
            myIcons.Add(newIcon);
            myIcons[i].GetComponent<CharacterPortrait>().characterController = Manager.instance.turnController.activeEntities[i].GetComponent<CharacterController>();
            myIcons[i].GetComponent<CharacterPortrait>().SetupPortrait();
        }
    }

    public void UpdateIcons()
    {

    }
}
