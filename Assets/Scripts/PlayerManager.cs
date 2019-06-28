using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get{ return _instance; }
    }

    [SerializeField] private List<PlayerData> players = new List<PlayerData>();
    public int PlayerCount => players.Count;

    private Dictionary<int, GameObject> inGameCharacters = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    public PlayerData AddPlayer(int deviceId, int number, Color color)
    {
        PlayerData newPlayer = new PlayerData(number, color, deviceId);
        players.Add(newPlayer);
        return newPlayer;
    }

    public void InstantiateCharacter(int deviceId, GameObject characterGO)
    {
        inGameCharacters.Add(deviceId, characterGO);
    }

    public void RemovePlayer(int deviceId)
    {
        PlayerData player = GetPlayerById(deviceId);
        if (player != null)
        {
            players.Remove(player);
        }
    }

    public PlayerData GetPlayer(int index)
    {
        if (index > -1 && index < players.Count)
            return players[index];
        return null;
    }

    public PlayerData GetPlayerById(int id)
    {
        for (int i = 0; i < players.Count; i++)
            if (players[i].inputDeviceId == id)
                return players[i];
        return null;
    }

    public int IndexOfPlayer(PlayerData player)
    {
        return players.IndexOf(player);
    }

    public void ClearPlayers()
    {
        players = new List<PlayerData>();
    }

    public GameObject GetPlayerCharacter(int deviceId)
    {
        return inGameCharacters[deviceId];
    }
}
