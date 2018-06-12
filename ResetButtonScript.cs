using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* When the reset button is clicked, it reloads the game
 */
public class ResetButtonScript : MonoBehaviour {
    
    void Start()
    {
        Button resetButton = GetComponent<Button>();
        resetButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameManagerScript.freezePlayerControl = false;
        SceneManager.LoadScene(0);
    }
    
}
