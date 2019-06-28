using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public Vector3 rotationSpeed;

    void LateUpdate()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
