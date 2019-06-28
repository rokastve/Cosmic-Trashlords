using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float radius = 30;
    public float speed = 50;
    public float time = 4;
    Vector3 newPosition;

    private bool isCoroutineExecuting = false;
    private Transform spaceship;

    private void Start()
    {
        spaceship = GameObject.Find("Spaceship").transform;
    }

    private void Update()
    {
        StartCoroutine(ExecuteAfterTime(time));
        float step = speed * Time.deltaTime;
        Transform trans = gameObject.GetComponent<Transform>();
        trans.position = Vector3.MoveTowards(transform.position, newPosition, step);
        Vector3 offset = trans.position - new Vector3(0f, 0f, 0f);
        offset.Normalize();
        offset = offset * radius;
        trans.position = Vector3.MoveTowards(transform.position, offset, step);
        trans.LookAt(spaceship);
    }

    private Vector3 RandomPointOnCircleEdge()
    {
        var Vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(Vector2.x, 0, Vector2.y);
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        radius = Random.Range(30f, 40f);
        newPosition = RandomPointOnCircleEdge();

        isCoroutineExecuting = false;
    }
}
