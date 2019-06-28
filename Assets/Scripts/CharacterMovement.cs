using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterInput))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private CharacterController controller;
    private CharacterInput input;

    private float gravity = 9.8f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<CharacterInput>();
    }

    void Update()
    {
        Vector3 moveDirection = GetCameraRelativeMovement(new Vector3(input.InputDirection.x, input.InputDirection.y));
        Vector3 final = moveDirection * moveSpeed;
        float verticalVelocity = controller.velocity.y;
        if (!controller.isGrounded)
            verticalVelocity -= gravity * Time.fixedDeltaTime;
        else
            verticalVelocity = -controller.stepOffset / Time.fixedDeltaTime;
        final.y = verticalVelocity;
        controller.Move(final * Time.deltaTime);
        
    }

    Vector3 GetCameraRelativeMovement(Vector3 inputDirection)
    {
        Vector3 dir = Camera.main.transform.TransformDirection(inputDirection);
        dir.y = 0;
        dir.Normalize();
        return dir;
    }

    public Vector3 GetVelocity()
    {
        return controller.velocity;
    }
}
