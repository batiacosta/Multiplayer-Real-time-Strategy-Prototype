using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class UIGameOverDisplay : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplayParent = null;
    [SerializeField] private TextMeshProUGUI winnerNameText = null;
    private void Start()
    {
        GameOverHandler.ClientOnGameOver += GameOverHandler_ClientOnGameOver;
    }

    private void OnDestroy()
    {
        GameOverHandler.ClientOnGameOver += GameOverHandler_ClientOnGameOver;
    }

    public void LeaveGame()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            //  Stop hosting
            NetworkManager.singleton.StopHost();
        }
        else
        {
            //  Stop client
            NetworkManager.singleton.StopClient();
        }
    }

    private void GameOverHandler_ClientOnGameOver(string winnerName)
    {
        winnerNameText.text = $"{winnerName} Has Won!";
        gameOverDisplayParent.gameObject.SetActive(true);
    }
}
