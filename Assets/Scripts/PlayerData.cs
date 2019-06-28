using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public event Action<PlayerData> OnDataChanged;

    [NonSerialized]
    private bool isReady;
    public bool IsReady
    {
        get { return isReady; }
        set { isReady = value; OnDataChanged?.Invoke(this); }
    }

    [SerializeField]
    private int displayNumber;
    public int DisplayNumber
    {
        get { return displayNumber; }
        set { displayNumber = value; OnDataChanged?.Invoke(this); }
    }

    [SerializeField]
    private Color displayColor;
    public Color DisplayColor
    {
        get { return displayColor; }
        set { displayColor = value; OnDataChanged?.Invoke(this); }
    }

    [SerializeField] public readonly int inputDeviceId;

    public PlayerData(int number, Color selectedColor, int id)
    {
        this.displayNumber = number;
        this.displayColor = selectedColor;
        this.inputDeviceId = id;
    }
}