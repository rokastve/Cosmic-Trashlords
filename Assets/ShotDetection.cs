using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDetection : MonoBehaviour
{
    private EnemyHealth EH;
    void Start()
    {
        EH = GetComponentInParent<EnemyHealth>();   
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Projectile"))
        {
            EH.gotHit(2.5f);
        }
    }
}
