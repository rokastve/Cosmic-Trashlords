using UnityEngine;
using UnityEngine.Experimental.Input;

public class ClawOperable : OperableBase
{
    public ClawActions clawInputAsset;

    public Transform exitSpawnPoint;
    public Transform seatPoint;

    private ClawMovement clawMovement;

    public void Start()
    {
        Operator = null;
        base.input = new ClawActions(Instantiate(clawInputAsset.asset));
        SetupInputEvents();
        base.input.Disable();
        clawMovement = GetComponentInChildren<ClawMovement>();
    }

    public override bool Enter(int id)
    {
        if (base.Enter(id) == false)
            return false;

        //Make character sit down
        GameObject character = PlayerManager.Instance.GetPlayerCharacter(id);
        character.transform.position = seatPoint.position;
        character.GetComponentInChildren<CharacterVisuals>().transform.rotation = seatPoint.rotation;
        character.GetComponentInChildren<CharacterVisuals>().Sitting(true);

        //Make camera follow
        Camera.main.GetComponentInParent<CameraController>().AddTarget(GetComponentInChildren<Claw>().transform);
        return true;
    }

    public override void Exit(int id)
    {
        clawMovement.isRetracting = true;
        base.Exit(id);

        GameObject character = PlayerManager.Instance.GetPlayerCharacter(id);
        character.GetComponentInChildren<CharacterVisuals>().Sitting(false);
        character.transform.position = exitSpawnPoint.position;

        Camera.main.GetComponentInParent<CameraController>().RemoveTarget(GetComponentInChildren<Claw>().transform);
    }

    public void MovementPerformed(InputAction.CallbackContext obj) => clawMovement.InputDirection = obj.ReadValue<Vector2>();
    public void MovementCancelled(InputAction.CallbackContext obj) => clawMovement.InputDirection = Vector2.zero;

    public override void SetupInputEvents()
    {
        ClawActions clawInput = input as ClawActions;
        clawInput.Operable.Exit.performed += ExitPressed;
        clawInput.Operable.Movement.performed += MovementPerformed;
        clawInput.Operable.Movement.cancelled += MovementCancelled;
    }

    public override void ClearInputEvents()
    {
        ClawActions clawInput = base.input as ClawActions;
        clawInput.Operable.Exit.performed -= ExitPressed;
        clawInput.Operable.Movement.performed -= MovementPerformed;
        clawInput.Operable.Movement.cancelled -= MovementCancelled;
    }

    private void ExitPressed(InputAction.CallbackContext obj)
    {
        Exit(obj.control.device.id);
    }
}
