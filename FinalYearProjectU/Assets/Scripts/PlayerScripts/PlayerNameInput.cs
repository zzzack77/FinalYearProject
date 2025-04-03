using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    public PrivateVariables privateVariables;
    public string inputName;
    // Start is called before the first frame update
    void Start()
    {
        privateVariables = GetComponent<PrivateVariables>();
        OnNameEnterPress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnNameEnterPress()
    {
        if (inputName.Length < 9)
        {
            privateVariables.PlayerName = "bobbyy";

        }
        else
        {
            // Enter a shorter name error message appear
        }
    }
}
