using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    public void AddScore(int i)
    {
        int scr = PlayerPrefs.GetInt("CurrentScore");
        scr = scr + i;
        PlayerPrefs.SetInt("CurrentScore", scr);
    }
}
