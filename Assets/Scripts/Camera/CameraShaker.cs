using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    private Vector3 startPos;

    public void Shake(float amount, float duration)
    {
        StartCoroutine(ShakeAnimation(amount, duration));
    }

    IEnumerator ShakeAnimation(float amount, float duration)
    {
        Vector3 startPos = transform.localPosition;
        float t = duration;
        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            transform.localPosition = startPos + Random.insideUnitSphere * amount * t;
            yield return null;
        }
        transform.localPosition = startPos;
    }
}
