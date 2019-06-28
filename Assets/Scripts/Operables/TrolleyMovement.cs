using UnityEngine;

public class TrolleyMovement : MonoBehaviour
{
    public float topSpeed = 1f;
    public float inertia = 1f;
    public Rails rails;

    private float velocity = 0.0f;
    private float currentPos = 5.0f;

    void Start()
    {
        // Setup start orientation
        transform.position = rails.GetPosition(currentPos);
        transform.rotation = rails.GetRotation(currentPos);
    }

    void Update()
    {
        // Get input vector
        Vector3 inputVector = new Vector3(
            Input.GetAxisRaw("Horizontal"), 
            0.0f,
            Input.GetAxisRaw("Vertical")).normalized;

        // Sample bezier curve and project input vector onto it
        float direction = rails.GetDirectionProjected(currentPos, inputVector);

        // Trolley movement
        velocity += direction * inertia * Time.deltaTime * topSpeed;
        velocity = Mathf.Clamp(velocity, -topSpeed, topSpeed);
        currentPos += velocity * Time.deltaTime / rails.GetLengthMultiplier(currentPos);
        velocity = Mathf.Lerp(velocity, 0.0f, inertia * Time.deltaTime);

        // Position and rotate the trolley
        transform.position = rails.GetPosition(currentPos);
        transform.rotation = rails.GetRotation(currentPos);
    }
}
