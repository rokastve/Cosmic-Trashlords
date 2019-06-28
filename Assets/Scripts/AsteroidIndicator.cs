using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidIndicator : MonoBehaviour
{
    public RectTransform targetIndicator;
    Vector3 targetPosition;

    void Start()
    {
        targetIndicator = Instantiate(targetIndicator, GetComponentInChildren<Canvas>().transform, false);
        targetIndicator.gameObject.SetActive(false);
    }

    void Update()
    {
        targetPosition = gameObject.transform.position;
        SetIndicatorPosition(targetIndicator, targetPosition);
        if (targetIndicator.gameObject.activeSelf == false)
            targetIndicator.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the position and visibility for an offscreen indicator for one of the moving objects.
    /// </summary>
    /// <param name="indicator">The indicator image.</param>
    /// <param name="objectPos">Object position.</param>
    void SetIndicatorPosition(RectTransform indicator, Vector3 objectPos)
    {
        // Convert world position to screen position
        Vector3 objectScreenPos = Camera.main.WorldToScreenPoint(objectPos);

        // Clamp the obhject position to screen boundaries
        Vector3 indicatorPos = new Vector3();
        indicatorPos.x = Mathf.Clamp(objectScreenPos.x, 0, Screen.width);
        indicatorPos.y = Mathf.Clamp(objectScreenPos.y, 0, Screen.height);
        indicatorPos.z = objectScreenPos.z;

        // If the object is offscreen, we need to draw the indicator.
        if (objectScreenPos != indicatorPos)
        {
            // Adjust indicator position to be fully on-screen, instead of halfway off-screen.
            indicatorPos.x = Mathf.Clamp(objectScreenPos.x, indicator.rect.width, Screen.width - indicator.rect.width);
            indicatorPos.y = Mathf.Clamp(objectScreenPos.y, indicator.rect.height, Screen.height - indicator.rect.height);
            indicatorPos.z = 0.0F;

            // Point the indicator in the direction of the off-screen object,
            // and move it to the correct location.
            Vector3 indicatorDir = objectScreenPos - indicatorPos;
            indicator.transform.up = indicatorDir.normalized;
            indicator.rotation = Quaternion.Euler(0f, 0f, indicator.rotation.z);

            indicator.transform.position = indicatorPos;

            // Enable the indicator object to get it to show
            indicator.gameObject.SetActive(true);
        }
        else
        {
            // Disable the indicator object to get it to hide
            indicator.gameObject.SetActive(false);
        }
    }
    /// <summary>
	/// Caches the target object's position to draw an indicator on GUI updates.
	/// </summary>
	/// <param name="position">Position.</param>
	public void ShowTargetIndicator(Vector3 position)
    {
        targetPosition = position;
    }
}
