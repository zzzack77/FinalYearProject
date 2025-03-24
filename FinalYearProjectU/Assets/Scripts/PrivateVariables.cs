using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateVariables : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerDataManager playerDataManager;

    private int globalScore;
    private string playerName;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerDataManager = GetComponent<PlayerDataManager>();

        LoadDataToPrivateVariables();
    }
    public void LoadDataToPrivateVariables()
    {
        PlayerName = playerDataManager.getPlayerName();
        GlobalScore = playerDataManager.getPlayerScore();

    }
    public int GlobalScore { get => globalScore; set { globalScore = value; if (playerManager != null) { playerManager.UpdateScore(value); } } }
    public string PlayerName
    {
        get => playerName;

        set
        {
            playerName = value;
            playerDataManager.SetPlayerName(value);
        }
    } 

}
