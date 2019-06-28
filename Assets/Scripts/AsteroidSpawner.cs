using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject Asteroid;
    public float MinTime;
    public float MaxTime;
    public float MinSpeed;
    public float MaxSpeed;
    public float radius;
    private bool isSpawning;

    void Awake()
    {
        isSpawning = false;
    }

    void Update()
    {
        if (!isSpawning)
        {
            float timer = Random.Range(MinTime, MaxTime);
            Invoke("SpawnObject", timer);
            isSpawning = true;
            
        }
    }

    void SpawnObject()
    {
        Vector3 startR = Random.insideUnitCircle * radius;
        Vector3 endR = Random.insideUnitCircle * radius;
        Vector3 start = RandomCircle(transform.position, radius);
        Vector3 end = RandomCircle(transform.position, radius);
        GameObject clone = Instantiate(Asteroid, start, transform.rotation);
        Rigidbody rigidBody = clone.GetComponent<Rigidbody>();
        float speed = Random.Range(MinSpeed, MaxSpeed);
        rigidBody.useGravity = false;
        rigidBody.isKinematic = false;
        Vector3 force = (end-start).normalized;
        rigidBody.AddForce(force * speed);
        isSpawning = false;
    }
    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
