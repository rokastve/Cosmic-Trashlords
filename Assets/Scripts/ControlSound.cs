using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlSound : MonoBehaviour
{
    public Slider musicSlider;
    public Slider audioSlider;
    public AudioSource[] Music;
    public AudioSource[] Audio;
    public bool Ingame;

    public void Awake()
    {
        audioSlider.value = PlayerPrefs.GetFloat("AudioVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if(Ingame)
        {
            foreach (AudioSource music in Music)
            {
                music.volume = musicSlider.value;
            }
            foreach (AudioSource audio in Audio)
            {
                audio.volume = audioSlider.value;
            }
        }
        musicSlider.onValueChanged.AddListener(delegate { MusicValueChangeCheck(); });
        audioSlider.onValueChanged.AddListener(delegate { AudioValueChangeCheck(); });
        
    }

    // Invoked when the value of the slider changes.
    public void MusicValueChangeCheck()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        if (Ingame)
        {
            foreach (AudioSource music in Music)
            {
                music.volume = musicSlider.value;
            }
        }
    }
    public void AudioValueChangeCheck()
    {
        PlayerPrefs.SetFloat("AudioVolume", audioSlider.value);
        if (Ingame)
        {
            foreach (AudioSource audio in Audio)
            {
                audio.volume = audioSlider.value;
            }
        }
    }
}
