using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateVariables : MonoBehaviour
{
    private PlayerManager playerManager;

    private int globalScore;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        GlobalScore = GlobalScore;
    }

    public int GlobalScore { get => globalScore; set { globalScore = value; if (playerManager != null) { playerManager.UpdateScore(value); } } }

}
