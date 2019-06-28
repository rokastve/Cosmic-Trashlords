using UnityEngine;
using UnityEngine.Experimental.Input;

public class TurretOperable : OperableBase
{
    public TurretActions turretActionsInputAsset;

    public Transform seatPoint;
    public Transform exitPoint;
    public Transform cameraTarget;

    private Turret turret;

    public void Start()
    {
        Operator = null;
        base.input = new TurretActions(Instantiate(turretActionsInputAsset.asset));
        SetupInputEvents();
        base.input.Disable();
        turret = GetComponentInChildren<Turret>();
    }

    public override bool Enter(int id)
    {
        if (base.Enter(id) == false)
            return false;
        turret.Paused = false;

        //Make character sit
        GameObject character = PlayerManager.Instance.GetPlayerCharacter(id);
        character.transform.position = seatPoint.position;
        character.GetComponentInChildren<CharacterVisuals>().transform.rotation = seatPoint.rotation;
        character.GetComponentInChildren<CharacterVisuals>().Sitting(true);

        //Zoom out camera
        Camera.main.GetComponentInParent<CameraController>().AddTarget(cameraTarget);
        return true;
    }

    public override void Exit(int id)
    {
        turret.Paused = true;
        base.Exit(id);

        GameObject character = PlayerManager.Instance.GetPlayerCharacter(id);
        character.GetComponentInChildren<CharacterVisuals>().Sitting(false);
        character.transform.position = exitPoint.position;

        Camera.main.GetComponentInParent<CameraController>().RemoveTarget(cameraTarget);
    }

    //Input events
    private void AimingPerformed(InputAction.CallbackContext obj) => turret.InputDirection = obj.ReadValue<Vector2>();
    private void AimingCancelled(InputAction.CallbackContext obj) => turret.InputDirection = Vector2.zero;

    public override void SetupInputEvents()
    {
        TurretActions turretInput = base.input as TurretActions;
        turretInput.Operable.Exit.performed += ExitPressed;
        turretInput.Operable.Aiming.performed += AimingPerformed;
        turretInput.Operable.Aiming.cancelled += AimingCancelled;
    }

    public override void ClearInputEvents()
    {
        TurretActions turretInput = base.input as TurretActions;
        turretInput.Operable.Exit.performed -= ExitPressed;
        turretInput.Operable.Aiming.performed -= AimingPerformed;
        turretInput.Operable.Aiming.cancelled -= AimingCancelled;
    }

    private void ExitPressed(InputAction.CallbackContext obj)
    {
        Exit(obj.control.device.id);
    }
}
