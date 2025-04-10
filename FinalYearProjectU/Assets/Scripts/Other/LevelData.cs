using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game Data/Level Data")]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class LevelInfo
    {
        public int level;
        public int requiredXP;
    }

    public List<LevelInfo> levels;
}