using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    [Range(1f, 89f)]
    public float angle = 60f;
    public float minDistance = 20f;
    public float maxDistance = 50f;
    public float furthestApart = 12f;
    public float smoothness = 16f;

    private List<Transform> targets = new List<Transform>();
    private float currDistance;
    private Vector3 targetPos;
    private Vector3 smoothVel;

    void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Bounds bounds = GetTargetBounds();
        Vector3 center = bounds.center;

        float apart = Mathf.Max(bounds.size.x, bounds.size.z);
        currDistance = Mathf.Lerp(minDistance, maxDistance, apart / furthestApart);

        float rad = (90f - angle) * Mathf.Deg2Rad;
        float z = Mathf.Sin(rad) * currDistance;
        float y = Mathf.Cos(rad) * currDistance;

        targetPos = center + new Vector3(0f, y, -z);
        transform.eulerAngles = Vector3.right * angle;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref smoothVel, Time.deltaTime * smoothness);
    }

    public void AddTarget(Transform target)
    {
        targets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
    }

    Bounds GetTargetBounds()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
            bounds.Encapsulate(targets[i].position);
        return bounds;
    }
}
