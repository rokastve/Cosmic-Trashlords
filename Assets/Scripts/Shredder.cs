using UnityEngine;

public class Shredder : MonoBehaviour
{
    public GameObject particle;
    public Transform spawnPoint;
    public float yForce;
    public float zForce;
    public float randomXForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trash"))
        {
            Scrap scrap = other.GetComponent<Scrap>();
            if(scrap != null)
            {
                Vector3 enterPosition = new Vector3(scrap.transform.position.x, spawnPoint.position.y, spawnPoint.position.z);
                Vector3 randomForce = new Vector3(Random.Range(-randomXForce, randomXForce), yForce, Random.Range(zForce * 0.5f, zForce));
                scrap.EnterGravity(enterPosition, randomForce);
                Instantiate(particle, enterPosition, Quaternion.identity);
            }
        }
    }
}
