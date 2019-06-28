using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.Self);
    }
}
