using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}
