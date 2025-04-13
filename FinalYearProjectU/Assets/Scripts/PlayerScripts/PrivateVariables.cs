using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateVariables : MonoBehaviour
{
    private QuickQuizManager quickQuizManager;
    private PlayerDataManager playerDataManager;
    private MenuManager menuManager;

    public LevelData levelData;

    private string playerName;
    private int currentGold;
    [SerializeField]
    private bool[] unlockedPets = new bool[8];
    private string currentPet;
    private int currentLevel;
    private int globalXP;

    
    [SerializeField]
    private int operatorType;
    [SerializeField]
    private int maxNumRange;
    private float timeRemaining = 20f;
    private int totalNumberOfQuestions = 10;
    [SerializeField]
    private int questionNumber;
    [SerializeField]
    private int numberOfCorrectQuestions;

    private void Start()
    {
        quickQuizManager = GetComponent<QuickQuizManager>();
        playerDataManager = GetComponent<PlayerDataManager>();
        menuManager = GetComponent<MenuManager>();

        LoadDataToPrivateVariables();
    }
    
    public void LoadDataToPrivateVariables()
    {
        playerName = playerDataManager.GetPlayerName();
        currentGold = playerDataManager.GetPlayerGold();
        unlockedPets = playerDataManager.GetPlayerUnlockedPets();
        currentPet = playerDataManager.GetPlayerPet();
        currentLevel = playerDataManager.GetPlayerLevel();
        globalXP = playerDataManager.GetPlayerXP();

        CheckLevelUp(GlobalXP);
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
    public int CurrentGold
    {
        get => currentGold;
        set
        {
            currentGold = value;
            menuManager.UpdateGold();
            playerDataManager.SetPlayerGold(value);
            
        }
    }
    public bool[] UnlockedPets
    {
        get => unlockedPets;
        set
        {
            Debug.Log("unlocked");
            unlockedPets = value;
            playerDataManager.SetPlayerUnlockedPets(value);
        }
    }
    public void UnlockPet(int index)
    {
        if (index < 0 || index >= unlockedPets.Length) return;

        unlockedPets[index] = true;

        if (playerDataManager != null)
            playerDataManager.SetPlayerUnlockedPets(unlockedPets);
    }
    public string CurrentPet
    {
        get => currentPet;
        set
        {
            currentPet = value;
            menuManager.UpdatePet(value);
            playerDataManager.SetPlayerPet(value);
        }
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
    public int MaxNumRange { get => maxNumRange; set => maxNumRange = value; }
    public int OperatorType {  get => operatorType; set => operatorType = value; }
    public float TimeRemaining { get => timeRemaining; set => timeRemaining = value; }
    public int TotalNumberOfQuestions { get => totalNumberOfQuestions; set => totalNumberOfQuestions = value; }
    public int QuestionNumber { get => questionNumber; set => questionNumber = value; }
    public int NumberOfCorrectQuestions { get => numberOfCorrectQuestions; set => numberOfCorrectQuestions = value; }
    void CheckLevelUp(int currentXP)
    {
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
        return "";
    }
}
