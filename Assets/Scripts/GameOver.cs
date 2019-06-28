using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject overPanel;
    [SerializeField] private Text GameOverText;
    WriteScore score;
    void Start()
    {
        overPanel.SetActive(false);
        score = FindObjectOfType<WriteScore>();
    }

    public void End()
    {
        overPanel.SetActive(true);
        Time.timeScale = 0;
        score.Write(PlayerPrefs.GetInt("CurrentScore"));
        GameOverText.text = PlayerPrefs.GetInt("CurrentScore").ToString();
    }
}
