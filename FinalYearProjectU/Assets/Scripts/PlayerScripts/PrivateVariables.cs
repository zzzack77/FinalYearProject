using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateVariables : MonoBehaviour
{
    private QuickQuizManager quickQuizManager;
    private PlayerDataManager playerDataManager;
    private MenuManager menuManager;

    public LevelData levelData;

    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int globalXP;
    private string playerName;
    [SerializeField]
    private int operatorType;
    private int maxNumRange;
    private float timeRemaining;

    private void Start()
    {
        quickQuizManager = GetComponent<QuickQuizManager>();
        playerDataManager = GetComponent<PlayerDataManager>();
        menuManager = GetComponent<MenuManager>();

        LoadDataToPrivateVariables();
    }
    
    public void LoadDataToPrivateVariables()
    {

        currentLevel = playerDataManager.getPlayerLevel();
        globalXP = playerDataManager.getPlayerXP();
        playerName = playerDataManager.getPlayerName();

    }
    public int CurrentLevel
    {
        get => currentLevel;
        set
        {
            currentLevel = value;
            menuManager.UpdateLevel();
            playerDataManager.SetPlayerLevel(value);
        }
    }
    public int GlobalXP 
    { 
        get => globalXP;
        set 
        { 
            globalXP = value;
            menuManager.UpdateLevel();
            CheckLevelUp(value);
            playerDataManager.SetPlayerXP(value);
        } 
    }
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

    void CheckLevelUp(int currentXP)
    {
        Debug.Log("Current xp: " + currentXP);
        // Loop through levels to find the right one
        for (int i = 0; i < levelData.levels.Count; i++)
        {
            //Debug.Log(levelData.levels[i].requiredXP);
            if (currentXP >= levelData.levels[i].requiredXP)
            {
                CurrentLevel = levelData.levels[i].level;
            }
            else
            {
                break;
            }
        }
    }
    public float CalculatePercentageXP()
    {
        if (currentLevel > 0)
        {
            float currentLevelXP = levelData.levels[currentLevel - 1].requiredXP;
            float nextLevelXP = levelData.levels[currentLevel].requiredXP;
            float value = (GlobalXP - currentLevelXP) / (nextLevelXP - currentLevelXP);
            return value * 100;
        }
        else return 0;
    }
    public string CalculateRemainingXP()
    {
        if (currentLevel > 0)
        {
            int currentLevelXP = levelData.levels[currentLevel - 1].requiredXP;
            int nextLevelXP = levelData.levels[currentLevel].requiredXP;

            return (GlobalXP - currentLevelXP).ToString() + " / " + (nextLevelXP - currentLevelXP).ToString();
        }
        return "0 / " + levelData.levels[currentLevel].ToString();
        //return 
        //if (i == 0)
        //{
        //    return GlobalXP - currentLevelXP;
        //}
        //if (i == 1)
        //{
        //    return nextLevelXP - currentLevelXP;
        //}
        //else return 0;
    }
}
