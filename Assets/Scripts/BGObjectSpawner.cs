using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObjectSpawner : MonoBehaviour
{
    public float displacement = 30;
    public GameObject obj;
    public float minTime;
    public float maxTime;
    float randTime = 30;
    Vector3 spawnerPos;
    Vector3 spawnPos;

    private void Start()
    {
        spawnerPos = gameObject.transform.position;
        randTime = Random.Range(minTime, maxTime);
    }
    private void Update()
    {
        randTime -= Time.deltaTime;
        if (randTime <= 0)
        {
            randTime = Random.Range(minTime, maxTime);
            float randX = spawnerPos.x - Random.Range(-displacement, displacement);
            spawnPos = new Vector3(randX, spawnerPos.y, spawnerPos.z);
            Instantiate(obj, spawnPos, gameObject.transform.rotation);
        }
    }
}
