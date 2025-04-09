using System.IO;
using UnityEngine;

// Class to store player information
[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int currentLevel;
    public int xp ;
}

// Manager to handle saving and loading player data
public class PlayerDataManager : MonoBehaviour
{
    private string filePath;

    void Awake()
    {
        // Set the file path where the player data will be stored
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");

        // Debug log to check the file path
        Debug.Log("Persistent Data Path: " + filePath);
    }

    // Save player data to a JSON file
    public void SavePlayerData(PlayerData playerData)
    {
        if (playerData == null)
        {
            Debug.LogError("Error: Player data is null!");
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("Error: filePath is null or empty!");
            return;
        }

        // Convert the PlayerData object to JSON
        string json = JsonUtility.ToJson(playerData, true);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Serialization failed: JSON is empty.");
            return;
        }

        // Write the JSON to the file
        File.WriteAllText(filePath, json);
        //Debug.Log("Player data saved successfully at: " + filePath);
    }

    // Load player data from a JSON file
    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found! Returning new player data.");
            return new PlayerData();
        }

        string json = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Error: Loaded JSON is empty.");
            return new PlayerData();
        }

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        return playerData;
    }
    public void SetPlayerLevel(int level)
    {
        if (GetPlayerDataManager())
        {
            PlayerData player = LoadPlayerData();
            player.currentLevel = level;
            SavePlayerData(player);
        }
    }
    public int getPlayerLevel()
    {
        if (GetPlayerDataManager())
        {
            PlayerData player = LoadPlayerData();
            return player.currentLevel;
        }
        return 0;
    }
    public void SetPlayerXP(int xp)
    {
        if (GetPlayerDataManager())
        {
            PlayerData player = LoadPlayerData();
            player.xp = xp;
            SavePlayerData(player);
        }
    }
    public int getPlayerXP()
    {
        if (GetPlayerDataManager())
        {
            PlayerData player = LoadPlayerData();
            return player.xp;
        }
        return 0;
    }

    public void SetPlayerName(string name)
    {
        if (GetPlayerDataManager())
        {
            PlayerData player = LoadPlayerData();
            player.playerName = name;
            SavePlayerData(player);
        }
    }
    public string getPlayerName()
    {
        if (GetPlayerDataManager())
        {
            PlayerData player = LoadPlayerData();
            return player.playerName;
        }
        return null;
    }
    public bool GetPlayerDataManager()
    {
        // Find an existing PlayerDataManager in the scene
        PlayerDataManager manager = FindObjectOfType<PlayerDataManager>();

        if (manager == null)
        {
            Debug.LogError("PlayerDataManager not found in the scene!");
            return false;
        }
        return true;
    }
}
