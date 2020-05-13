using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.PlayerManager;

public class GameMaster : MonoBehaviour
{
    public GameObject playerManagerGO;

    private PlayerManager playerManager;
    private Transform _playerGO;
    private bool _isNewPlayer = true;

    private void Awake()
    {
        StartGame(_isNewPlayer);
        DontDestroyOnLoad(this);
    }

    private void StartGame(bool isNewPlayer)
    {
        playerManager = playerManagerGO.GetComponent<PlayerManager>();

        if(isNewPlayer)
        {
            _playerGO = playerManager.SpawnPlayer("Player1", isNewPlayer);
            _playerGO.parent = playerManagerGO.transform;
        }
    }
}