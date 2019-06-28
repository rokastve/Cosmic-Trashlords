using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceOnClick : MonoBehaviour
{
    public Vector3 force = new Vector3(0f, 10f, 0f);

    private void OnMouseDown()
    {
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(Vector3.right * 90f, ForceMode.Acceleration);
    }
}
