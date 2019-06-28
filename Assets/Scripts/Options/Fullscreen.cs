using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    public Toggle myToggle;
    public bool first = true;
    private int FSH;
    private int FSW;
    private int WH;
    private int WW;

    public void Start()
    {
        FSH = Screen.currentResolution.height;
        FSW = Screen.currentResolution.width;
        WH = Screen.height;
        WW = Screen.width;
        if(Screen.fullScreen)
        {
            myToggle.isOn = true;
        }
        else
        {
            myToggle.isOn = false;
        }
        first = !first;
    }
    public void change()
    {
        if (!first)
        {
            Screen.fullScreen = !Screen.fullScreen;
            if (Screen.fullScreen)
            {
                Screen.SetResolution(WW, WH, false);
            }
            else
            {
                Screen.SetResolution(FSW, FSH, true);
            }
        }
    }
}
