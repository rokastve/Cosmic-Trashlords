using System.Collections.Generic;
using UnityEngine;

public class GravityOnTrigger : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;
    public float multiplier = 10f;

    private void Start()
    {
        Float();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        for (int i = 0; i < rigidbodies.Count; i++)
            rigidbodies[i].useGravity = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Float();
    }

    private void Float()
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].useGravity = false;
            rigidbodies[i].AddForce(new Vector3(Random.Range(-1, 1), 2f * multiplier, Random.Range(-1, 1)));
        }
    }
}
