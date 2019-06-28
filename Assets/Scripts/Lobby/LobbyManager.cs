using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public string sceneToLoad = "";
    public int minPlayersToStart = 2;
    public List<Color32> availableColors;

    private void Start()
    {
        PlayerManager.Instance.ClearPlayers();
    }

    public void Join(LobbyActions actions, int deviceId)
    {
        //Debug.Log(deviceId + " pressed JOIN");

        PlayerData player = PlayerManager.Instance.GetPlayerById(deviceId);
        if (player == null)
        {
            //Joining lobby
            Color32 playerColor = availableColors[0];
            availableColors.RemoveAt(0);
            player = PlayerManager.Instance.AddPlayer(deviceId, PlayerManager.Instance.PlayerCount + 1, playerColor);
            GetComponent<LobbyUI>().AddLobbySlot(player);
            UpdatePlayerNumbers();

            //TODO: Setup other input actions
            //actions.General.Next..
        }
        else
        {
            //Setting to ready
            player.IsReady = true;
            CheckReady();
        }
    }

    public void StartTheGame()
    {
        Debug.Log("Starting the game");
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Back(int deviceId)
    {
        PlayerData player = PlayerManager.Instance.GetPlayerById(deviceId);
        if(player != null)
        {
            if (player.IsReady)
            {            
                //Set player not ready
                player.IsReady = false;
            }
            else
            {
                //Player leaves lobby slot
                availableColors.Add(player.DisplayColor);
                GetComponent<LobbyUI>().RemoveLobbySlot(player);
                PlayerManager.Instance.RemovePlayer(deviceId);
                UpdatePlayerNumbers();
            }
        }
    }

    private void UpdatePlayerNumbers()
    {
        int count = PlayerManager.Instance.PlayerCount;
        for(int i = 0; i < count; i++)
        {
            PlayerData player = PlayerManager.Instance.GetPlayer(i);
            player.DisplayNumber = PlayerManager.Instance.IndexOfPlayer(player) + 1;
        }
    }

    public void CheckReady()
    {
        //Debug.Log("Checking ready");
        if(PlayerManager.Instance.PlayerCount >= minPlayersToStart)
        {
            //Debug.Log("Enough players to start the game");
            if (AllReady())
            {
                //Debug.Log("ALL READY!");
                InputSystem.DisableAllEnabledActions();
                FindObjectOfType<LobbyCountdown>().StartCountdown();
            }
        }
    }

    private bool AllReady()
    {
        for (int i = 0; i < PlayerManager.Instance.PlayerCount; i++)
            if (PlayerManager.Instance.GetPlayer(i).IsReady == false)
                return false;
        return true;
    }
}