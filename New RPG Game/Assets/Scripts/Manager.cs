using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance { get; private set; }

    [Header("ActionSystem")]
    public ActionInterface actionInterface = null;
    public ActionManager actionManager = null;

    [Header("TurnSystem")]
    public TurnController turnController = null;
    public TurnOrderQueInterface turnOrderQueInterface = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // DontDestroyOnLoad(gameObject);
    }
}

