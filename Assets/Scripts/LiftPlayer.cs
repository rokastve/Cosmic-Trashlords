using UnityEngine;

public class LiftPlayer : MonoBehaviour
{
    public float liftForce = 1000f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * liftForce);
        }
    }
}
