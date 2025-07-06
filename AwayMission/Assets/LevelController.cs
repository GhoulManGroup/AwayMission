using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Move this to after sceneloaded when we add new scenes and such but for now its here
        Debug.Log("Start Turn");
        Manager.instance.turnController.SetupTurnController();
    }

    //Later add stuff for win / loss conditions ect when we plan out what those are>?

}
