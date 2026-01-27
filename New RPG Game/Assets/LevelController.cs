using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Navigation;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update

    public LevelState levelState;

    public void Start()
    {
        Manager.instance.levelController = this;
    }

    public enum LevelState
    {
        explore, // when free move using nav mesh is enabled
        combat, // when the player should be locked onto grid movenet.
    }

    public void BeginCombat()
    {
        Debug.Log("Swapping To Combat Test");
        if (levelState == LevelState.explore)
        {
            levelState = LevelState.combat;

            Manager.instance.gridManager.GridState(true);

            Manager.instance.turnController.SetupTurnController();
        }
    }

   }

