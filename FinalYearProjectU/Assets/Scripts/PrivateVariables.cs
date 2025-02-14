using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateVariables : MonoBehaviour
{
    private int score;
    
    public int Score { get => score; set => score = value; }

}
