using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class CreditsScroll : MonoBehaviour
{
    public string sceneToLoad;
    public float scrollSpeed = 25f;
    public float endOffset = 1000f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.Translate(Vector3.up * Time.deltaTime * scrollSpeed, Space.Self);
        if (rect.anchoredPosition.y > rect.rect.height + endOffset)
            SceneManager.LoadScene(sceneToLoad);
    }
}
