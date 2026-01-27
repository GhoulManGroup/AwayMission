using Navigation;
using PartyManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{ /// <summary>
/// This class is a Core script that you use to access all the various key scripts in the project from one another more conveniently to reduce unecessary varibles / refrences 
/// The purpose of this class is not to access every script but to allow classes that need to contact other non related systems
/// </summary>
    public static Manager instance { get; private set; }

    [Header("PartyManagement")]
    public PartyController partyController = null;

    [Header("LevelController")]
    public LevelController levelController = null;

    [Header("ActionSystem")]
    public ActionInterface actionInterface = null;
    public ActionManager actionManager = null;

    [Header("TurnSystem")]
    public TurnController turnController = null;
    public TurnOrderQueInterface turnOrderQueInterface = null;

    [Header("Combat Navigation System")]
    public PathFinder pathFinder = null;
    public GridManager gridManager = null;

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

