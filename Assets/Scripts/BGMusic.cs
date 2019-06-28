using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public List<AudioClip> soundtrack;
    AudioSource mySource;
    private void Awake()
    {
        mySource = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        if (!mySource.playOnAwake)
        {
            mySource.clip = soundtrack[Random.Range(0, soundtrack.Count)];
            mySource.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!mySource.isPlaying)
        {
            mySource.clip = soundtrack[Random.Range(0, soundtrack.Count)];
            mySource.Play();
        }
    }

}
