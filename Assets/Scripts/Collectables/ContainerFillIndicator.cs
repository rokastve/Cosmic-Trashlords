using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(CanvasGroup))]
public class ContainerFillIndicator : MonoBehaviour
{
    public Text fillProgressText;
    public Image fillProgress;

    public string pickupAnimation;
    public string dropAnimation;

    private CanvasGroup group;
    private Animation anim;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0.0f;
        anim = GetComponent<Animation>();
    }

    public void Pickup(CharacterContainer container)
    {
        if (container.IsEmpty == false)
        {
            StartCoroutine(Fade(1.0f));
        }
        anim.Play(pickupAnimation);
        UpdateProgress(container);
    }

    public void Drop(CharacterContainer container)
    {
        anim.Play(dropAnimation);
        if (container.IsEmpty)
        {
            StartCoroutine(Fade(0f));
        }
        else
            UpdateProgress(container);
    }

    private IEnumerator Fade(float target)
    {
        while(Mathf.Abs(target - group.alpha) > 0.1f)
        {
            group.alpha = Mathf.MoveTowards(group.alpha, target, Time.deltaTime * 5f);
            yield return null;
        }
        group.alpha = target;
    }

    private void UpdateProgress(CharacterContainer container)
    {
        fillProgress.fillAmount = container.FillPercentage;
        fillProgressText.text = string.Format("{0}/{1}", container.Amount, container.maxCapacity);
    }
}
