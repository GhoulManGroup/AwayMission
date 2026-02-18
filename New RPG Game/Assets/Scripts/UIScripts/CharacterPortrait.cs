using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CombatSystem;

public class CharacterPortrait : MonoBehaviour
{
    //Use for both turn order and party UI portrait
    public EntityController characterController;

    public Image myPortrait;
    public Image greyOutSprite;

    public TMPro.TextMeshProUGUI myText;

    public TMPro.TextMeshProUGUI myInitative;
    
    public void SetupPortrait()
    {
        myPortrait.sprite = characterController.myCharacter.myIcon;
        myText.text = characterController.myCharacter.health.ToString();
        myInitative.text = characterController.currentInitative.ToString();
        characterController.myPortrait = this.gameObject;
    }

    public void UpdatePortrait()
    {
        myPortrait.sprite = characterController.myCharacter.myIcon;
        myText.text = characterController.myCharacter.health.ToString();
        myInitative.text = characterController.currentInitative.ToString();
        characterController.myPortrait = this.gameObject;

        this.greyOutSprite.enabled = characterController.hasActed;
    }

    public void RemovePortrait()
    {
        Destroy(this.gameObject);
    }
      
}
