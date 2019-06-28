using UnityEditor;
using UnityEngine;

public class BezierCurve : MonoBehaviour, ICurve
{
    public Transform[] controlPoints;
    public float lengthMultiplier = 1.0f;

    [Header("Debug")]
    [SerializeField] private int steps = 8;
    [SerializeField] private float tangentSampleDist = 0.01f;

    private void OnDrawGizmos()
    {
        if (controlPoints.Length < 4)
            return;

        //Draw control lines
        //Handles.color = Color.red;
        //Handles.DrawSolidDisc(controlPoints[1].position, Vector3.up, 0.1f);
        //Handles.DrawSolidDisc(controlPoints[2].position, Vector3.up, 0.1f);
        //Handles.DrawDottedLine(controlPoints[0].position, controlPoints[1].position, 2f);
        //Handles.DrawDottedLine(controlPoints[2].position, controlPoints[3].position, 2f);

        //Draw curve
        //Handles.color = Color.white;
        //Handles.DrawWireDisc(controlPoints[0].position, Vector3.up, 0.25f);
        //Handles.DrawWireDisc(controlPoints[3].position, Vector3.up, 0.25f);
        Vector3 lastPos = controlPoints[0].position;
        for (float t = 0.0f; t < 1.0f; t += (1.0f / steps))
        {
            Vector3 pos = GetPosition(t);
            Gizmos.DrawLine(lastPos, pos);
            lastPos = pos;
        }
        Gizmos.DrawLine(lastPos, controlPoints[3].position);
    }

    public Vector3 GetPosition(float t)
    {
        Vector3 pos = Mathf.Pow(1.0f - t, 3.0f) * controlPoints[0].position +
                3.0f * Mathf.Pow(1.0f - t, 2.0f) * t * controlPoints[1].position +
                3.0f * (1.0f - t) * t * t * controlPoints[2].position +
                t * t * t * controlPoints[3].position;
        return pos;
    }

    public Vector3 GetTangent(float t)
    {
        Vector3 from = GetPosition(t);
        Vector3 to = GetPosition(t + tangentSampleDist);
        return (to - from).normalized;
    }

    public Quaternion GetRotation(float t)
    {
        return Quaternion.LookRotation(GetTangent(t), Vector3.up);
    }

    public float GetDirectionProjected(float t, Vector3 vec)
    {
        Vector3 tangent = GetTangent(t);
        float direction = Vector3.Dot(vec.normalized, tangent);
        return direction;
    }
}

public interface ICurve
{
    Vector3 GetPosition(float t);
    Quaternion GetRotation(float t);
    float GetDirectionProjected(float t, Vector3 vec);
}
