using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * When the Button on the popup window is clicked, the popup window will start shrinking
 */
public class PopupButtonScript : MonoBehaviour
{
    public PopupWindowScript popupWindow;
    

    void Start()
    {
        Button resetButton = GetComponent<Button>();
        resetButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        popupWindow.StartShrinking();
    }
}
