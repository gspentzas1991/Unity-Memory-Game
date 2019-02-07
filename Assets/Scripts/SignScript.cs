using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* When a sign is facedown and is clicked, it will turn face up. If another sign has been clicked, the two
 * signs get compared, otherwise the sign passes its type to the GameManagerScript. When two signs are compared
 * if they are not of the same type, a sound effect plays and they both turn face down. If they are of the same type
 * a sound effect plays, the two signs stay face up, and a popup is created with the sign sprite and text of the two
 * matched signs. A sign that is face up or is rotating, can not be clicked. The sprite of a sign changes when its
 * rotation is greater than 90 degrees, to reaveal its image.
 */
public class SignScript : MonoBehaviour {

    //The script of the text in the textbox bellow the sign
    public SignTextScript signText;
    //The sprite of the sign with the revealed image
    public Sprite revealedSprite;
    public Sprite emptySprite;
    public GameObject popupWindow;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public float turningSpeed;
    //The type of the sign e.g. "SPAM"
    public string signType;
    //Becomes true when the player clicks on the sign. A sign that is selected can't be clicked again
    bool isSelected=false;
    //Becomes true when the sign starts rotating to its facedown rotation. A sign can't be selected while true
    bool resetSign = false;
    AudioSource audioManager;
    SpriteRenderer spriteRenderer;
    //The Quaternion rotations for when a sign is face up or face down
    Quaternion facingDownRotation;
    Quaternion facingUpRotation;

    //Initializes variables and detaches the Sign Textbox
	void Start ()
    {
        audioManager = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        facingUpRotation = Quaternion.Euler(0f, 179.9f, 0f);
        facingDownRotation = transform.rotation;
        //After they are initialized, we don't want the Sign Textbox to be a child of Sign
        transform.DetachChildren();
	}

    //Rotates the sign as necessary
    void Update()
    {
        //Reveals the sprite image when the sign passes the 90 degrees
        if (transform.rotation.eulerAngles.y > 90)
            spriteRenderer.sprite = revealedSprite;
        if (transform.rotation.eulerAngles.y < 90)
            spriteRenderer.sprite = emptySprite;
        //resets the resetSign flag when the sign completes its facedown rotation
        if ((transform.rotation.eulerAngles.y == 0f) && (resetSign))
            resetSign = false;
        //Rotates the sign as necessary
        if (resetSign)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, facingDownRotation, turningSpeed);
        else if (isSelected)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, facingUpRotation, turningSpeed);
        }
    }
    
    /* Trigger for when the sign is clicked. If two signs get clicked, they get compared here
     */
    void OnMouseDown()
    {
        if (!GameManagerScript.freezePlayerControl)
        {
            //Only signs facing down can be clicked
            if (transform.rotation.eulerAngles.y == 0f)
            {
                //Only signs that are not already clicked, can be clicked
                if (!isSelected)
                {
                    /* If a sign exists on GameManagerScript.signToCompare, the two signs get compared
                     * Otherwise this sign gets passed as a value to GameManagerScript.signToCompare
                    */
                    if (GameManagerScript.signToCompare != null)
                    {
                        /* If the two signs are of the same type, a match is made.
                         * After the comparison, GameManagerScript.signToCompare becomes null
                        */
                        if (GameManagerScript.signToCompare.signType.Equals(signType))
                        {
                            StartCoroutine(PauseAndCreatePopup(1));
                            StartCoroutine(EnableSignText(0.5f, GameManagerScript.signToCompare));
                            StartCoroutine(PlaySound(0.5f, true));

                        }
                        else
                        {
                            StartCoroutine(PauseAndResetSign(1f));
                            StartCoroutine(GameManagerScript.signToCompare.PauseAndResetSign(1f));
                            StartCoroutine(PlaySound(0.5f, false));
                        }
                        GameManagerScript.signToCompare = null;
                    }
                    else
                    {
                        GameManagerScript.signToCompare = this;
                    }
                }
                isSelected = true;
            }
        }
    }

    //After timeToStop seconds, the text on the textboxes of this sign, and the secondSign, get enabled
    IEnumerator EnableSignText(float timeToStop, SignScript secondSign)
    {

        yield return new WaitForSeconds(timeToStop);
        signText.GetComponent<Text>().enabled = true;
        secondSign.signText.GetComponent<Text>().enabled = true;
    }

    //After timeToStop seconds, plays a sound, regarding if the match was correct or not
    IEnumerator PlaySound(float timeToStop,bool correct)
    {
        yield return new WaitForSeconds(timeToStop);
        if(correct)
        {
            audioManager.clip = correctSound;
        }
        else
        {
            audioManager.clip = wrongSound;
        }
        audioManager.Play();
    }

    /* Freezes player control, and after timeToStop seconds, creates a popup window, sets its scale, and
    *  passes to it the necessary data to display
    */
    IEnumerator PauseAndCreatePopup(float timeToStop)
    {

        GameManagerScript.freezePlayerControl = true;
        yield return new WaitForSeconds(timeToStop);
        GameObject popup = Object.Instantiate(popupWindow);
        popup.transform.localScale = new Vector3(0.0012f, 0.0008f, 1);
        PopupWindowScript popupScript=popup.GetComponent<PopupWindowScript>();
        popupScript.SetPopupData(revealedSprite, signType);

    }

    /* Freezes player control, and after timeToStop seconds, sets the flag for the sign to begin
     * rotating face down
     */
    public IEnumerator PauseAndResetSign(float timeToStop)
    {
        GameManagerScript.freezePlayerControl = true;
        yield return new WaitForSeconds(timeToStop);
        resetSign = true;
        isSelected = false;
        GameManagerScript.freezePlayerControl = false;
    }

    //Sets the text of the textbox below the sign
    public void SetTextboxContents(string signTextboxContents)
    {
       signType = signTextboxContents;
       signText.SetText(signTextboxContents);
    }
}
