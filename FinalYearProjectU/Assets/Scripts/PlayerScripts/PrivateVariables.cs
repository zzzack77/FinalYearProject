using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateVariables : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerDataManager playerDataManager;

    private int globalScore;
    private string playerName;

    private int operatorType;
    private int maxNumRange;
    private float timeRemaining;


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
    public int MaxNumRange { get => maxNumRange; set => maxNumRange = value; }
    public int OperatorType {  get => operatorType; set => operatorType = value; }
    public float TimeRemaining { get => timeRemaining; set => timeRemaining = value; }

}
