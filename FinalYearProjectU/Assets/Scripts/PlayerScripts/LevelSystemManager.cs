using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystemManager : MonoBehaviour
{
    public LevelData levelData;

    //public int currentXP = 0;
    public int currentLevel = 1;

    //public void AddXP(int amount)
    //{
    //    currentXP += amount;
    //    //CheckLevelUp();
    //}

    void CheckLevelUp(int currentXP)
    {
        // Loop through levels to find the right one
        for (int i = 0; i < levelData.levels.Count; i++)
        {
            if (currentXP >= levelData.levels[i].requiredXP)
            {
                currentLevel = levelData.levels[i].level;
            }
            else
            {
                break;
            }
        }
    }
}
