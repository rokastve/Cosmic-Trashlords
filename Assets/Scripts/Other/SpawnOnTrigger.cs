using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    public GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
