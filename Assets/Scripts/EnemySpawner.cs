using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool isCoroutine1Executing = false;
    private bool isCoroutine2Executing = false;
    private float radius = 50f;
    private float initTime = 10f;
    private Vector3 spawnPosition;

    public float minSpawnTime = 15f;
    public float maxSpawnTime = 20f;
    public GameObject Enemy;

    void Update()
    {
        initTime = Random.Range(minSpawnTime, maxSpawnTime);
        StartCoroutine(ExecuteAfterTimeSpawn(initTime));
    }

    IEnumerator ExecuteAfterTimeSpawn(float time)
    {
        if (isCoroutine1Executing)
            yield break;

        isCoroutine1Executing = true;

        yield return new WaitForSeconds(time);

        spawnPosition = RandomPointOnCircleEdge();
        Instantiate(Enemy, spawnPosition, new Quaternion(90f,1f,1f,1f));

        isCoroutine1Executing = false;
    }

    private Vector3 RandomPointOnCircleEdge()
    {
        var Vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(Vector2.x, 0, Vector2.y);
    }
}
