using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public float speed = 0.5f;
    public BezierCurve curve;

    private void Start()
    {
        transform.position = curve.GetPosition(0.0f);
        transform.rotation = curve.GetRotation(0.0f);
    }

    void Update()
    {
        float progress = Mathf.Lerp(0.0f, 1.0f, (Time.time * speed) % 1.0f);
        transform.position = curve.GetPosition(progress);
        transform.rotation = curve.GetRotation(progress);
    }
}
