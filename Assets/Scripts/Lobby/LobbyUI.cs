using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Input;

public class LobbyUI : MonoBehaviour
{
    public Transform slotsParent;
    public Transform iconsParent;
    public Color joinedColor;

    [Header("Prefabs")]
    public GameObject inputDeviceIconPrefab;
    public GameObject slotPrefab;

    [Header("Icons")]
    public Sprite iconGamepad;
    public Sprite iconKeyboard;

    private void Start()
    {
        SetupInputDeviceIcons();
    }

    private void SetupInputDeviceIcons()
    {
        foreach(InputDevice device in InputSystem.devices)
            if(device is Keyboard || device is Gamepad)
            {
                GameObject deviceIcon = Instantiate(inputDeviceIconPrefab, iconsParent);
                deviceIcon.name = device.id.ToString();
                deviceIcon.GetComponent<Image>().sprite = device is Keyboard ? iconKeyboard : iconGamepad;
            }
    }

    public void AddLobbySlot(PlayerData player)
    {
        //Change alpha of device icon
        iconsParent.Find(player.inputDeviceId.ToString()).GetComponent<Image>().color = joinedColor;

        //Instantiate slot
        LobbySlot slot = Instantiate(slotPrefab, slotsParent).GetComponent<LobbySlot>();
        slot.name = player.inputDeviceId.ToString();
        slot.Setup(player);
    }

    public void RemoveLobbySlot(PlayerData player)
    {
        LobbySlot slot = slotsParent.Find(player.inputDeviceId.ToString()).GetComponent<LobbySlot>();
        if (slot == null)
            Debug.LogError("Trying to remove slot that doesn't exist: " + player.inputDeviceId);

        iconsParent.Find(player.inputDeviceId.ToString()).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
        Destroy(slot.gameObject);
    }
}
