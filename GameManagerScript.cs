using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* The GameManager is an object that always exists in the scene. When the game starts it creates the signs, and
 * it has static values to hold necessary information e.g. what sign the player clicked previously. When a
 * popup window gets created, the GameManager enables a darkenScreen UI image, to darken the screen behind the popup
 */
public class GameManagerScript : MonoBehaviour
{

    public Transform positionOfFirstSign;
    public GameObject signPrefab;
    public int horizontalNumberOfSigns;
    public int verticalNumberOfSigns;
    //How far the signs are from each other, verticaly and horizontaly
    public float horizontalSignOffset;
    public float verticalSignOffset;
    //A UI element that darkens the screen, when the popup appears
    public Image darkenScreen;
    //Saves the sign that the player clicks on, in order to be compared with the next sign he clicks
    public static SignScript signToCompare =null;
    /* When true, the player can't click on signs.Becomes true by SignScript 
     * when two signs have been selected and by PopupWindowScript while it exists
    */
    public static bool freezePlayerControl = false;
    //When true, the darkenScreen Image is enabled. Set by PopupWindowScript
    public static bool popupExists = false;

    /*Arrays containing data for the signs. Each row of all three arrays, contains data for the same sign type
    * e.x. if signSprites[5] contains the sprite for the "SPAM" sign, signTextboxContents[5] should also
    * have the string "SPAM"  and signCounter[5] should keep count of how many times a "SPAM" sign was created 
    */
    public Sprite[] signSprites = new Sprite[12];
    public string[] signTextboxContents = new string[12];
    int[] signCounter = new int[12];

    //Creates the signs and passes to them data, randomly
	void Start ()
    {
        for (int i = 0; i < horizontalNumberOfSigns; i++)
        {
            for (int j = 0; j < verticalNumberOfSigns; j++)
            {
                GameObject newSign = Instantiate(signPrefab, positionOfFirstSign.position + new Vector3(horizontalSignOffset * i, -verticalSignOffset * j, 0f), new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
                SignScript newSignScript = newSign.GetComponent <SignScript>();
                int randomPointer=Random.Range(0, 12);
                while (signCounter[randomPointer]>=2)
                {
                    randomPointer = Random.Range(0, 12);
                }
                signCounter[randomPointer]++;
                newSignScript.SetTextboxContents(signTextboxContents[randomPointer]);
                newSignScript.revealedSprite = signSprites[randomPointer];
            }
        }
	}
	
    void Update()
    {
        if (popupExists)
            darkenScreen.enabled = true;
        else
            darkenScreen.enabled = false;
    }
    
}
