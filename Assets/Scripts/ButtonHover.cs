using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip AudioSelect;
    public AudioClip AudioConfirm;
    private AudioSource audioSource;
    private Outline outline;

    void Start()
    {
        outline = gameObject.GetComponentInChildren<Outline>();
        outline.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        outline.enabled = true;
        audioSource.PlayOneShot(AudioSelect);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }
    public void OnPointerDown(PointerEventData ped)
    {
        audioSource.PlayOneShot(AudioConfirm);
    }
}