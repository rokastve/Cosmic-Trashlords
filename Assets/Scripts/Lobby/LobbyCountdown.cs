using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCountdown : MonoBehaviour
{
    public float countdownDuration = 3f;
    public GameObject countdownPanel;
    public Text secondsText; 
    public string sceneToLoad;

    public void StartCountdown()
    {
        countdownPanel.SetActive(true);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float timer = countdownDuration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
                timer = 0f;
            secondsText.text = timer.ToString("F2");
            yield return null;
        }

        FindObjectOfType<LobbyManager>().StartTheGame();
    }
}
