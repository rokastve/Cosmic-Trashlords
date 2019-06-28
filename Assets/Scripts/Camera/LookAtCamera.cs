using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(target);
    }
}
