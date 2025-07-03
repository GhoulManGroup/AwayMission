using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : Button
{
    public Action myAction;

    public void PressedButton()
    {
        //ASSUME we have checked the action can be done will need more progress before the code exists to permit this test like enemys or pathfinding to be done ect.

        Manager.instance.actionManager.action = myAction;
        Manager.instance.actionInterface.ActionBarState(false);

    }

}
