using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UIDocument document;

    private Label globalScoreL;
    private Button[] numPadButtons = new Button[10];

    private Label Q1;
    private Label Q2;
    private int storedQuestion1;
    private int storedQuestion2;

    private TextField inputHolder;
    private Button clearB;
    private Button enterB;

    [SerializeField]   
    private int currentStoredNumber;
    [SerializeField]
    private int currentCorrectAnswer;

    void Start()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        
        document = GetComponent<UIDocument>();
        if (document != null)
        {
            // Root
            VisualElement root = document.rootVisualElement;

            // Initialize score and set empty text
            globalScoreL = root.Q<Label>("GlobalScore");
            if (globalScoreL != null) { globalScoreL.text = "Score: "; }

            // Initilize question labels and set first questions
            Q1 = root.Q<Label>("Q1");
            Q2 = root.Q<Label>("Q2");
            ResetRandomQuestions();

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
                else { Debug.LogError($"Button B{i} not found!"); }
            }

            // Clear num pad input
            clearB = root.Q<Button>("ClearB");
            if(clearB != null) { clearB.clicked += OnClearPress; }

            // Enter num pad input
            enterB = root.Q<Button>("EnterB");
            if (enterB != null) { enterB.clicked += OnEnterPress; }
        }
    }
    void ResetRandomQuestions()
    {
        if (Q1 != null && Q2 != null)
        {
            storedQuestion1 = Random.Range(0, 100);
            Q1.text = storedQuestion1.ToString();

            storedQuestion2 = Random.Range(0, 100);
            Q2.text = storedQuestion2.ToString();

            currentCorrectAnswer = storedQuestion1 + storedQuestion2;
        }
        else { Debug.LogError("Random Questions unable to be set"); }
    }

    void OnButtonPress(int buttonNumber)
    {
        //Debug.Log($"Button {buttonNumber} pressed");
        if (inputHolder != null) { inputHolder.value = inputHolder.value + buttonNumber; }
        GetCurrentStoredNumber(buttonNumber);
    }
    void OnClearPress()
    {
        if (inputHolder != null) { inputHolder.value = null; }
        currentStoredNumber = 0;
    }
    void OnEnterPress()
    {
        CheckAnswer();
    }
    void CheckAnswer()
    {
        if (currentCorrectAnswer == currentStoredNumber)
        {
            ResetRandomQuestions();
            OnClearPress();
        }
        else { StartCoroutine(FlashRed()); }
    }
    private IEnumerator FlashRed()
    {
        if (inputHolder != null)
        {
            inputHolder.style.color = Color.red;
            yield return new WaitForSeconds(1f);
            inputHolder.style.color = Color.black;
            OnClearPress();
        }
    }
    void GetCurrentStoredNumber(int value) { currentStoredNumber = (currentStoredNumber * 10) + value; }

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
