using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteScore : MonoBehaviour
{
    public void Write(int score)
    {
        string name = DateTime.Now.ToShortDateString();
        HighScoreManager._instance.SaveHighScore(name, score);
    }
}
