using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * The script that sets the text on the textbox below each sign
 */
public class SignTextScript : MonoBehaviour {

    Text signText;
    
    
    public void SetText(string text)
    {
        signText = GetComponent<Text>();
        signText.text = text;
    }
}
