using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsSelfDestruct : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
