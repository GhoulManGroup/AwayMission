using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    //Contains action passed from action interface belonging to active unit,
    //For each effect in that action this class will call effect system and await the resolution before concluding the action is done.
    //Will then return control back to action interface / permit passing priority to another unit in the turn sequence

    public CharacterController characterController;
    public Action action;

    [Header("Ability Casting System")]
    public bool checkingEffect = false;
    public int readyEffects = 0; //Prepared Effects of this Action awaiting completion
    public int effectsResolved = 0; //Effects that have been completed

    // Start is called before the first frame update
    void Awake()
    {
        Manager.instance.actionManager = this;
    }

    public IEnumerator StartAction()
    {
        for (int i = 0; i <action.actionEffects.Count; i++)
        {
            checkingEffect = true;
            this.GetComponent<ActionEffectSystem>().effectToResolve = action.actionEffects[i];
            this.GetComponent<ActionEffect>().StartCoroutine("PrepareAndCastEffect");

            while (checkingEffect == true)
            {
                yield return null;
            }
        }
        // Here We Are
        while (readyEffects != myAbility.abilityEffects.Count)
        {
            yield return null;
        }
    }
}
