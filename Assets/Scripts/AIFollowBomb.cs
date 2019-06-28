using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowBomb : MonoBehaviour
{
    GameObject[] players;
    GameObject closest;
    public ParticleSystem smoke;
    public float speed;
    private Rigidbody rb;
    private bool IsDetonating = false;
    private bool IsShot = false;
    private float index= 0;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Projectile"))
        {
            Shot();
            IsShot = true;
        }
    }
            public GameObject Closest(GameObject[] players)
    {
        GameObject go = new GameObject();
        float mindist = 999999f;
        foreach(GameObject obj in players)
        {
            float dist = Vector3.Distance(obj.transform.position, rb.transform.position);
            if(dist < mindist)
            {
                go = obj;
            }
        }
        return go;
    }
    void Shot()
    {
        FindObjectOfType<EnemyStack>().increase();
        index = FindObjectOfType<EnemyStack>().current;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
    void FixedUpdate()
    {
        if (!IsShot)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            closest = Closest(players);
            float dist = Vector3.Distance(closest.transform.position, rb.transform.position);
            if (dist <= 1f)
                Detonate();
            Vector3 force = (closest.transform.position - rb.position);
            rb.AddForce(force * speed);
        }
        else
        {
            Vector3 pos = new Vector3(closest.transform.position.x, 2+(index*1), closest.transform.position.z);
            gameObject.transform.position = pos;
        }
    }
    private void Detonate()
    {
        if (!IsShot)
        {
            if (!IsDetonating)
            {
                rb.isKinematic = true;
                IsDetonating = true;
                Instantiate(smoke, gameObject.transform);
                Invoke("Damage", 1);
                Destroy(gameObject, 1f);
            }
        }
    }
    private void Damage()
    {
        //FindObjectOfType<ShipHealth>().DecreaseHealth(1f);
    }
}