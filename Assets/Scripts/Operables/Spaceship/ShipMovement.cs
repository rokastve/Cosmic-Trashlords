using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float turningForce = 100f;
    public float force = 100f;

    public Vector2 InputDirection { get; set; }
    private bool paused = true;
    public bool Paused
    {
        get { return paused; }
        set
        {
            if (value)
                InputDirection = Vector2.zero;
            paused = value;
        }
    }

    public void Update()
    {
        if (paused)
            return;
        //Debug.Log(InputDirection);
        //float forwardAmount = Mathf.Clamp01(InputDirection.y);
        //transform.Translate(Vector3.forward * force * Time.deltaTime, Space.World);
        //transform.Rotate(Vector3.up * turningForce * Time.deltaTime);
        //rb.MovePosition(rb.position + transform.forward * force * forwardAmount * Time.fixedDeltaTime);
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * turningForce * InputDirection.x * Time.fixedDeltaTime));
    }
}
