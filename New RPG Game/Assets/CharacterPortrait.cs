using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{
    //Use for both turn order and party UI portrait
    public CharacterController characterController;

    public Image myPortrait;

    public TMPro.TextMeshProUGUI myText;
    
    public void SetupPortrait()
    {
        myPortrait.sprite = characterController.myCharacter.myIcon;
        myText.text = characterController.myCharacter.health.ToString();
        characterController.myPortrait = this.gameObject;
    }
      
}
