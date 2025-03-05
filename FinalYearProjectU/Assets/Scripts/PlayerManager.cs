using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    private UIDocument document;

    private Label globalScoreL;
    private Button[] numPadButtons = new Button[10];
    private Button clearB;
    private Button enterB;
    private TextField inputHolder;

    private void Update()
    {
        
    }
    void OnEnable()
    {
        Random.Range(0, 100);
        document = GetComponent<UIDocument>();
        if (document != null)
        {
            // Score
            VisualElement root = document.rootVisualElement;

            globalScoreL = root.Q<Label>("GlobalScore");

            if (globalScoreL != null)
            {
                globalScoreL.text = "Score: ";
            }

            // Input text holder for number input
            inputHolder = root.Q<TextField>("InputHolder");
            //InputHolder.value = "hello";

            // Num pad buttons 1 - 9
            for (int i = 0; i <= 9; i++)
            {
                numPadButtons[i] = root.Q<Button>("B" + i);
                if (numPadButtons[i] != null)
                {
                    int index = i;
                    numPadButtons[i].clicked += () => OnButtonPress(index);
                }
                else
                {
                    Debug.LogError($"Button B{i} not found!");
                }
            }
            // Clear num pad input
            clearB = root.Q<Button>("ClearB");
            if(clearB != null) { clearB.clicked += OnClearPress; }

            // Enter num pad input
            enterB = root.Q<Button>("EnterB");
            if (enterB != null) { enterB.clicked += OnEnterPress; }
        }
    }
    
    void OnButtonPress(int buttonNumber)
    {
        Debug.Log($"Button {buttonNumber} pressed");
        if (inputHolder != null) { inputHolder.value = inputHolder.value + buttonNumber; }
    }
    void OnClearPress()
    {
        if (inputHolder != null) { inputHolder.value = null; }
    }
    void OnEnterPress()
    {

    }
    public void UpdateScore(int score)
    {
        document = GetComponent<UIDocument>();
        if (document != null)
        {
            globalScoreL = document.rootVisualElement.Q<Label>("GlobalScore");

            if (globalScoreL != null)
            {
                globalScoreL.text = "Score: " + score;
            }
        }
    }
}
