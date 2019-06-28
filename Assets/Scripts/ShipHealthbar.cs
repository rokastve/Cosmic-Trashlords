using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthbar : MonoBehaviour
{
    [Header("Colors")]
    public Color foreColor;
    public Color regenColor;

    public float regenPulseSpeed = 4f;
    public float waitTime = 0.5f;
    public float decreaseSpeed = 0.8f;

    public Image[] bars;
    public Image[] barsForeground;

    private void OnEnable()
    {
        ShipHealth.OnHealthChanged += UpdateHeathbar;
    }

    private void OnDisable()
    {
        ShipHealth.OnHealthChanged -= UpdateHeathbar;
    }

    private void UpdateHeathbar(int currentBar, float fill, bool regen)
    {
        if(regen)
        {
            bars[currentBar].fillAmount = fill;
            barsForeground[currentBar].color = Color.Lerp(foreColor, regenColor, (Mathf.Sin(Time.time * regenPulseSpeed) / 2f + 0.5f));
            barsForeground[currentBar].fillAmount = fill;
            if (fill >= 1.0f)
                barsForeground[currentBar].color = foreColor;
        }
        else
        {
            barsForeground[currentBar].color = foreColor;
            barsForeground[currentBar].fillAmount = fill;
            StartCoroutine(DecreaseAnimation(currentBar, fill));
        }
    }

    IEnumerator DecreaseAnimation(int index, float fill)
    {
        yield return new WaitForSeconds(waitTime);
        while(bars[index].fillAmount > fill)
        {
            float fillAmount = Mathf.MoveTowards(bars[index].fillAmount, barsForeground[index].fillAmount, decreaseSpeed * Time.deltaTime);
            bars[index].fillAmount = fillAmount;
            yield return null;
        }
    }
}
