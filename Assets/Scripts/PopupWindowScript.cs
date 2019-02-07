using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* The popup window gets created by the SignScript when two signs get matched. The sign also passes
 * to the popup the sign image and sign text that the popup should display. The popup gets created with
 * small scalling, and every frame it enlarges until it reaches the desired scale. When the "Close Info" button
 * is pressed, the popup starts shrinking, and after a point it gets destroyed
 */
public class PopupWindowScript : MonoBehaviour {

    //The image component for the sign on the popup
    public Image popupSignImage;
    //The text component on the textbox of the popup
    public Text popupSignText;
    //The text component for the flavour text of the popup
    public Text popupSignFlavourText;
    //When true the popup window will start shrinking. Becomes true by the StartShrinking() method
    bool shrink = false;
    
    void Start ()
    {
        GameManagerScript.popupExists = true;
	}

    //Enlarges or shrinks the popup window, and destroys it if it becomes too small
    void Update()
    {

        float newLocalScaleX = transform.localScale.x;
        float newLocalScaleY = transform.localScale.y;
        if (!shrink)
        {
            if (transform.localScale.x < 0.016)
            {
                newLocalScaleX += 0.0004f;
            }
            if (transform.localScale.y < 0.008)
            {
                newLocalScaleY += 0.0002f;
            }

        }
        else
        {
            newLocalScaleX -= 0.0004f;
            newLocalScaleY -= 0.0002f;
        }
        transform.localScale = new Vector3(newLocalScaleX, newLocalScaleY, transform.localScale.z);
        if (transform.localScale.x < 0.0005f)
        {        
            GameManagerScript.popupExists = false;
            GameManagerScript.freezePlayerControl = false;
            Destroy(this.gameObject);
        }

    }

    //Sets the sign sprite and textbox text. Gets called by SignScript when a match of two signs is made.
    public void SetPopupData(Sprite signSprite, string signType)
    {
        popupSignImage.sprite = signSprite;
        popupSignText.text = signType;
        //Sets the flavour text, according to the type of sign
        switch(signType)
        {
            case "UNAUTHORIZED ACCESS":
                popupSignFlavourText.text = "Securely configure firewalls and keep software security updated.\n\nEncrypt sensitive information and use strong passwords.";
                break;
            case "MOBILE DEVICE ATTACK":
                popupSignFlavourText.text = "Encrypt all sensitive information and keep devices with you at all times.\n\nAvoid connecting to untrusted wireless and put Bluetooth in 'undiscoverable' mode.";
                break;
            case "SYSTEM COMPROMISE":
                popupSignFlavourText.text = "Install the latest security updates and patch all known vulnerabilities.\n\nSecurely configure and harden all systems and regularly scan for vulnerabilities.";
                break;
            case "CYBER ESPIONAGE":
                popupSignFlavourText.text = "Be alert for social engineering attempts and verify requests for sensitive information.\n\nSecurely configure your network and monitor for any unusual behavior.";
                break;
            case "SOCIAL ENGINEERING":
                popupSignFlavourText.text = "Verify requests for sensitive information and never share passwords with anyone.\n\nDon't part with information if in any doubt and report all suspicious activity.";
                break;
            case "SPAM":
                popupSignFlavourText.text = "Only give your email address to trusted individuals and don't post it online.\n\nUse a spam filter and never reply to, or click links in spam emails.";
                break;
            case "MALWARE":
                popupSignFlavourText.text = "Don't open attachments or click links in emails from unknown or untrusted senders.\n\nNever install dubious software and keep your malware prevention up to date.";
                break;
            case "INSIDERS":
                popupSignFlavourText.text = "Restrict sensitive information access to only those that need it.\n\nReport suspicious activity or workers immediately.";
                break;
            case "DENIAL OF SERVICE":
                popupSignFlavourText.text = "Securely configure and harden networks against known DoS attacks.\n\nImplement intrusion detection and monitor for unusual network behavior.";
                break;
            case "DATA LEAKAGE":
                popupSignFlavourText.text = "Ensure sensitive information on laptops, mobiles and removable media is encrypted.\n\nCheck email recipients before sending and be mindful of information you post online.";
                break;
            case "PHISING":
                popupSignFlavourText.text = "Watch for sensitive information requests and never click on suspicious looking links.\n\nBe wary of contextually relevant emails from unknown senders.";
                break;
            case "IDENTITY THEFT":
                popupSignFlavourText.text = "Never provide personal information to untrusted individuals or websites.\n\nProtect personal information in storage and securely dispose of it.";
                break;
        }
    }

    //The PopupButtonScript calls this method
    public void StartShrinking()
    {
        //The Popup window should start shrinking, only if the button is
        //pressed when the popup window is at full size.
        if(transform.localScale.x> 0.015)
            shrink = true;
    }
}
