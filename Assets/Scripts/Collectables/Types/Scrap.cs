using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Scrap : MonoBehaviour, ICollectable
{
    public float maxTorqueInCosmos = 1f;
    private Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Spawned()
    {
        float x = Random.Range(-maxTorqueInCosmos, maxTorqueInCosmos);
        float y = Random.Range(-maxTorqueInCosmos, maxTorqueInCosmos);
        float z = Random.Range(-maxTorqueInCosmos, maxTorqueInCosmos);
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.AddTorque(x, y, z);
        rb.velocity = -Vector3.forward;
    }

    public void EnterGravity(Vector3 position, Vector3 force)
    {
        rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
        gameObject.layer = LayerMask.NameToLayer("Trash");
        GetComponent<Collider>().enabled = false;
        transform.position = position;
        GetComponent<Collider>().enabled = true;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.angularDrag = 0.1f;
        rb.AddForce(force, ForceMode.Impulse);
        GetComponentInChildren<Renderer>().enabled = true;
    }

    public void Pickup()
    {
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;
        rb.isKinematic = true;
    }

    public void Drop(Vector3 position, Vector3 force)
    {
        GetComponent<Collider>().enabled = true;
        GetComponentInChildren<Renderer>().enabled = true;
        rb.velocity = force;
        rb.isKinematic = false;
        transform.position = position;
    }
}
