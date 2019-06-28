using UnityEngine;
using UnityEngine.UI;

public class LobbySlot : MonoBehaviour
{
    [SerializeField] private Color readyColor;
    [SerializeField] private Color notReadyColor;
    [SerializeField] private Text statusText;
    [SerializeField] private Image statusBar;

    [SerializeField] private Image label;
    [SerializeField] private Text playerNumber;

    public void Setup(PlayerData player)
    {
        player.OnDataChanged += UpdateSlot;
        SetNotReady();
        UpdateSlot(player);
    }

    public void UpdateSlot(PlayerData player)
    {
        label.color = player.DisplayColor;
        playerNumber.text = "P" + player.DisplayNumber.ToString();
        if (player.IsReady)
            SetReady();
        else SetNotReady();
    }

    private void SetNotReady()
    {
        statusText.text = "NOT READY";
        statusText.color = notReadyColor;
        statusBar.color = notReadyColor;
    }

    private void SetReady()
    {
        statusText.text = "READY";
        statusText.color = readyColor;
        statusBar.color = readyColor;
    }
}