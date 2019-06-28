using System;
using UnityEngine;

public class Rails : MonoBehaviour, ICurve
{
    public BezierCurve[] curves;

    private int GetCurveIndex(float t)
    {
        int index = Mathf.FloorToInt(t);
        return index % curves.Length;
    }

    public float GetDirectionProjected(float t, Vector3 vec)
    {
        t = ClampProgress(t);
        int index = GetCurveIndex(t);
        return curves[index].GetDirectionProjected(t % 1.0f, vec);
    }

    public Vector3 GetPosition(float t)
    {
        t = ClampProgress(t);
        int index = GetCurveIndex(t);
        return curves[index].GetPosition(t % 1.0f);
    }

    public Quaternion GetRotation(float t)
    {
        t = ClampProgress(t);
        int index = GetCurveIndex(t);
        return curves[index].GetRotation(t % 1.0f);
    }

    private float ClampProgress(float t)
    {
        if (t < 0.0f)
        {
            t = t % curves.Length;
            t += (float)curves.Length;
        }
        return t;
    }

    public float GetLengthMultiplier(float t)
    {
        int index = GetCurveIndex(t);
        return curves[index].lengthMultiplier;
    }
}
