using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.TransformDirection(Vector3.forward)));
        }
    }

}
