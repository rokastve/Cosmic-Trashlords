using UnityEngine;
using UnityEngine.Experimental.Input;

public class HydraulicPressOperable : OperableBase
{
    public HydraulicPressActions inputActionsAsset;
    public Transform seatPoint;

    private HydraulicPress press;

    private void Start()
    {
        Operator = null;
        base.input = new HydraulicPressActions(Instantiate(inputActionsAsset.asset));
        SetupInputEvents();
        base.input.Disable();
        press = GetComponentInChildren<HydraulicPress>();
    }

    public override bool Enter(int id)
    {
        if (base.Enter(id) == false)
            return false;
        press.Paused = false;

        GameObject character = PlayerManager.Instance.GetPlayerCharacter(id);
        character.transform.position = seatPoint.position;
        character.GetComponentInChildren<CharacterVisuals>().transform.rotation = seatPoint.rotation;
        return true;
    }

    public override void Exit(int id)
    {
        press.Paused = true;
        base.Exit(id);
    }

    private void PressPressingPerformed(InputAction.CallbackContext context) => press.PressDirection = context.ReadValue<float>();
    private void PressPressingCancelled(InputAction.CallbackContext context) => press.PressDirection = 0f;

    public override void SetupInputEvents()
    {
        HydraulicPressActions pressInput = base.input as HydraulicPressActions;
        pressInput.Operable.Exit.performed += ExitPressed;
        pressInput.Operable.PressProgress.performed += PressPressingPerformed;
        pressInput.Operable.PressProgress.cancelled += PressPressingCancelled;
    }

    public override void ClearInputEvents()
    {
        HydraulicPressActions pressInput = base.input as HydraulicPressActions;
        pressInput.Operable.Exit.performed -= ExitPressed;
        pressInput.Operable.PressProgress.performed -= PressPressingPerformed;
        pressInput.Operable.PressProgress.cancelled -= PressPressingCancelled;
    }


    private void ExitPressed(InputAction.CallbackContext obj)
    {
        Exit(obj.control.device.id);
    }
}
