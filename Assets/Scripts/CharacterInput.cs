using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.Users;

public class CharacterInput : MonoBehaviour
{
    public int Id { get; private set; }
    public Vector2 InputDirection { get; private set; }

    public event Action<int> UsePressed = delegate { };//Debug.Log("Use"); };
    public event Action<int> BackPressed = delegate { };// Debug.Log("Back"); };

    private CharacterActions characterActions = null;
    private InputActionAssetReference currentInputActions = null;

    public void SetupInput(CharacterActions characterInputActions, int id)
    {
        Id = id;
        this.characterActions = characterInputActions;
        UseDefaultActionMap();

        //Setup events
        this.characterActions.General.Movement.performed += (e) => InputDirection = e.ReadValue<Vector2>();
        this.characterActions.General.Movement.cancelled += (e) => InputDirection = Vector2.zero;
        this.characterActions.General.Use.performed += (e) => UsePressed(Id);
        this.characterActions.General.Back.performed += (e) => BackPressed(Id);
    }

    public void UseActionMap(InputActionAssetReference otherInputActions)
    {
        StartCoroutine(SwitchOnNextFrame(otherInputActions));
    }

    public void UseDefaultActionMap()
    {
        UseActionMap(characterActions);
    }

    private IEnumerator SwitchOnNextFrame(InputActionAssetReference otherInputActions)
    {
        yield return null;
        InputDevice device = InputSystem.GetDeviceById(Id);
        InputUser? user = InputUser.FindUserPairedToDevice(device);
        if (user == null)
            Debug.LogError("InputUser is null");

        if (currentInputActions != null)
            currentInputActions.Disable();
        currentInputActions = otherInputActions;
        user.Value.AssociateActionsWithUser(otherInputActions);
        currentInputActions.Enable();
    }
}
