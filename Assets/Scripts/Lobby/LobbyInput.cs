using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.Users;

public class LobbyInput : MonoBehaviour
{
    [SerializeField] private LobbyActions actionsAsset = null;

    private void Start()
    {
        InputSystem.onDeviceChange += InputDeviceChanged;
        SetupInputUsers();
    }

    private void SetupInputUsers()
    {
        foreach (InputDevice device in InputSystem.devices)
            if (device is Gamepad || device is Keyboard)
            {
                Debug.Log("InputDevice found: " + device.displayName);
                AddUserInput(device);
            }
    }

    private void AddUserInput(InputDevice device)
    {
        //Pair user with input device
        InputUser user = InputUser.PerformPairingWithDevice(device);
        LobbyActions actions = new LobbyActions(Instantiate(actionsAsset.asset));
        user.AssociateActionsWithUser(actions);

        //Joining / Disconnecting from lobby
        LobbyManager lobby = GetComponent<LobbyManager>();
        actions.Enable();
        actions.General.Start.performed += (e) => lobby.Join(actions, device.id);
        actions.General.Back.performed += (e) => lobby.Back(device.id);
    }

    private void OnDestroy()
    {
        InputSystem.DisableAllEnabledActions();
    }

    private void RemoveUserInput(InputDevice device)
    {
        InputUser? user = InputUser.FindUserPairedToDevice(device);
        user.Value.UnpairDevicesAndRemoveUser();
    }

    private void InputDeviceChanged(InputDevice device, InputDeviceChange state)
    {
        switch (state)
        {
            case InputDeviceChange.Added:
                Debug.Log(device.name + ": is connected!");
                AddUserInput(device);
                break;
            case InputDeviceChange.Removed:
                Debug.Log(device.name + ": disconnected..");
                RemoveUserInput(device);
                break;
        }
    }
}
