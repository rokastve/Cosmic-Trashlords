using System;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public float waitBeforeRegenerating = 5f;
    public float regenerationRate = 0.02f;

    const int maxBars = 3;
    private int currentBarIndex = maxBars - 1;
    private float barFill = 1.0f;

    private float regenWaitTimer = 0.0f;

    public static event Action<int, float, bool> OnHealthChanged;

    private GameOver GameOverScript;

    private void Start()
    {
        GameOverScript = FindObjectOfType<GameOver>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
            //DecreaseHealth(0.4f);

        if(barFill < 1.0f)
        {
            if(regenWaitTimer < waitBeforeRegenerating)
                regenWaitTimer += Time.deltaTime;
            else
            {
                barFill += Time.deltaTime * regenerationRate;
                if (OnHealthChanged != null)
                    OnHealthChanged(currentBarIndex, barFill, true);
            }
        }
        
    }

    public void DecreaseHealth(float amount)
    {
        //Debug.Log(currentBarIndex);
        if (currentBarIndex < 0)
            return;
        regenWaitTimer = 0f;
        float left = barFill - amount;
        if (left < 0f)
        {
            if(OnHealthChanged != null)
                OnHealthChanged(currentBarIndex, 0f, false);
            currentBarIndex--;
            if (currentBarIndex < 0)
            {
                Debug.LogError("Game over");
                GameOverScript.End();
                return;
            }
            barFill = 1.0f;
        }
        else
            barFill -= amount;
        if (OnHealthChanged != null)
            OnHealthChanged(currentBarIndex, barFill, false);
        Camera.main.GetComponent<CameraShaker>().Shake(1f, 0.3f);
    }
}
