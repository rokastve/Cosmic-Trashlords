using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Globalization;
using System.Linq;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text text;
    void Start()
    {
        List<Scores> scores = HighScoreManager._instance.GetHighScore();
        Print(scores);
    }
    void Print(List<Scores> scores)
    {
        string test = "";
        int i = 1;
        foreach (Scores score in scores)
        {
            test = test + i+" " + score.name + " " + score.score + "\n";
            i++;
        }
        text.text = test;
    }

}

