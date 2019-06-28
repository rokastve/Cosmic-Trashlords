using System.Collections.Generic;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
    public float minX, maxX;
    public List<GameObject> trashPrefabs;
    public float spawnCooldown = 2f;

    private float cooldownTimer = 0.0f;

    private void LateUpdate()
    {
        if (cooldownTimer >= spawnCooldown)
        {
            int randomScrapIndex = Random.Range(0, trashPrefabs.Count);
            Vector3 randomSpawnPosition = transform.position + Vector3.right * Random.Range(minX, maxX);
            Scrap scrap = Instantiate(trashPrefabs[randomScrapIndex], randomSpawnPosition, Quaternion.identity).GetComponent<Scrap>();
            scrap.Spawned();
            cooldownTimer = 0.0f;
        }
        else
            cooldownTimer += Time.deltaTime;
    }
}
