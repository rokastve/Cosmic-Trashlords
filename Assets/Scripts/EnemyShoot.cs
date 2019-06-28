using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float minTimeToShoot = 1f;
    public float maxTimeToShoot = 5f;
    public GameObject projectilePrefab;
    public Transform[] spawnPoint;
    public Transform barrel;

    private float randTime;
    private bool isCoroutineExecuting = false;

    private void Start()
    {
        randTime = Random.Range(minTimeToShoot, maxTimeToShoot);
    }
    private void Update()
    {
        randTime -= Time.deltaTime;
        if (randTime <= 0)
        { 
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                Debug.Log("Pew");
                Instantiate(projectilePrefab, spawnPoint[i].position, Quaternion.LookRotation(barrel.TransformDirection(Vector3.forward)));
            }
            randTime = Random.Range(minTimeToShoot, maxTimeToShoot);
        }
    }

}
