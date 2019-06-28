using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10f;
    private Score scoreManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<Score>();
    }

    void Update()
    {
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void gotHit(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            scoreManager.AddScore(500);
            Destroy(gameObject);
        }
    }
}
