using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterVisuals : MonoBehaviour
{
    public float rotateSpeed = 720f;

    private CharacterMovement movement;
    private Animator anim;
    private Quaternion targetRotation;

    private void OnEnable()
    {
        movement = GetComponentInParent<CharacterMovement>();
        anim = GetComponent<Animator>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (anim.GetBool("Sitting"))
            return;
        if (anim.GetBool("Typing"))
            return;
        MovementAnimations();
    }

    private void MovementAnimations()
    {
        Vector3 vel = movement.GetVelocity();
        vel.y = 0f;
        if (vel != Vector3.zero)
            targetRotation = Quaternion.LookRotation(vel.normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        anim.SetFloat("MoveSpeed", vel.magnitude);
    }

    public void Sitting(bool state)
    {
        anim.SetBool("Sitting", state);
    }

    public void Typing(bool state)
    {
        anim.SetBool("Typing", state);
    }

    public void Carry(float amount)
    {
        anim.SetLayerWeight(1, amount);
    }
}
