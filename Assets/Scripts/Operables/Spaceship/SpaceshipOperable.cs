using System;
using UnityEngine;
using UnityEngine.Experimental.Input;

/*
public class SpaceshipOperable : OperableBase
{
    public OperableActions shipInputAsset;
    public Transform operatingPosition;
    public Transform cameraTarget;

    public ShipMovement shipMovement;

    void Start()
    {
        base.input = new OperableActions(Instantiate(shipInputAsset.asset));
        SetupInputEvents();
    }

    public override void Enter(int id)
    {
        base.Enter(id);
        shipMovement.Paused = false;

        GameObject character = PlayerManager.Instance.GetPlayerCharacter(id);
        character.transform.SetPositionAndRotation(operatingPosition.position, operatingPosition.rotation);
        character.GetComponentInChildren<CharacterVisuals>().Typing(true);

        Camera.main.GetComponentInParent<CameraController>().AddTarget(cameraTarget);
    }

    public override void Exit(int id)
    {
        shipMovement.Paused = true;
        base.Exit(id);

        PlayerManager.Instance.GetPlayerCharacter(id)
            .GetComponentInChildren<CharacterVisuals>().Typing(false);

        Camera.main.GetComponentInParent<CameraController>().RemoveTarget(cameraTarget);
    }

    private void MovementPerformed(InputAction.CallbackContext obj) => shipMovement.InputDirection = obj.ReadValue<Vector2>();
    private void MovementCancelled(InputAction.CallbackContext obj) => shipMovement.InputDirection = Vector2.zero;

    public override void SetupInputEvents()
    {
        OperableActions shipInput = input as OperableActions;
        shipInput.General.Back.performed += ExitPressed;
        shipInput.General.Movement.performed += MovementPerformed;
        shipInput.General.Movement.cancelled += MovementCancelled;
    }

    public override void ClearInputEvents()
    {
        OperableActions shipInput = input as OperableActions;
        shipInput.General.Back.performed -= ExitPressed;
        shipInput.General.Movement.performed -= MovementPerformed;
        shipInput.General.Movement.cancelled -= MovementCancelled;
    }

    private void ExitPressed(InputAction.CallbackContext obj)
    {
        Exit(obj.control.device.id);
    }

}
*/