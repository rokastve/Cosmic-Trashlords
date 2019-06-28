using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObjectMove : MonoBehaviour
{
    public float SurviveTime;
    public float speed;
    Vector3 GOPos;

    private void Start()
    {
        GOPos = gameObject.transform.position;
        Destroy(gameObject, SurviveTime);
    }
    private void Update()
    {
        Vector3 pos = new Vector3(GOPos.x, GOPos.y, GOPos.z - speed/10);
        gameObject.transform.position = pos;
        GOPos = gameObject.transform.position;
    }
}
