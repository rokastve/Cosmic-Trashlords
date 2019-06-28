using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public ParticleSystem ps;
    public float red = 0.0F;
    public float green = 0.0F;
    public float blue = 0.0F;
    public float alph = 1.0F;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var main = ps.main;
            main.startColor = new Color(red, green, blue,alph);
        }
    }
}
