using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SlidingDoor : MonoBehaviour
{
    public float slideSpeed = 3f;
    public string animBool = "IsOpen";

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", slideSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator.GetBool(animBool))
            return;
        animator.SetBool(animBool, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!animator.GetBool(animBool))
            return;
        animator.SetBool(animBool, false);
    }
}
