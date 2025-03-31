using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    private PrivateVariables privateVariables;
    public UIDocument uiDocument;
    public VisualTreeAsset additionUI;
    public VisualTreeAsset test;

    private Label globalScoreL;
    private Button[] numPadButtons = new Button[10];

    private Label Operator;

    private Label Q1;
    private Label Q2;
    private int storedQuestion1;
    private int storedQuestion2;

    private Label inputHolder;
    private Button clearB;
    private Button enterB;

    [SerializeField]   
    private int currentStoredNumber;
    [SerializeField]
    private int currentCorrectAnswer;
    private void Awake()
    {
        ShowUI(additionUI);
    }
    void Start()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiDocument = GetComponent<UIDocument>();

        privateVariables.OperatorType = 3;
        privateVariables.MaxNumRange = 100;


        OnStartMathsType();
    }
    public void OnStartMathsType()
    {
        if (uiDocument != null)
        {
            // Root
            VisualElement root = uiDocument.rootVisualElement;

            // Initialize score and set empty text
            globalScoreL = root.Q<Label>("GlobalScore");
            if (globalScoreL != null) { globalScoreL.text = "Score: "; }

            Operator = root.Q<Label>("QOperator");
            SetOperator();

            // Initilize question labels and set first questions
            Q1 = root.Q<Label>("Q1");
            Q2 = root.Q<Label>("Q2");
            ResetRandomQuestions();

            // Input text holder for number input
            inputHolder = root.Q<Label>("InputLabel");
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
            if (clearB != null) { clearB.clicked += OnClearPress; }

            // Enter num pad input
            enterB = root.Q<Button>("EnterB");
            if (enterB != null) { enterB.clicked += OnEnterPress; }
        }
    }
    void SetOperator()
    {
        //0 = +
        //1 = -
        //2 = *
        //3 = /
       int operatorType = privateVariables.OperatorType;
        if (Operator != null)
        {
            if (operatorType == 0) Operator.text = "+";
            else if (operatorType == 1) Operator.text = "-";
            else if (operatorType == 2) Operator.text = "*";
            else if (operatorType == 3) Operator.text = "/";
            else Operator.text = "0";
        }
    }
    void ResetRandomQuestions()
    {
        if (Q1 != null && Q2 != null)
        {
            storedQuestion1 = Random.Range(0, privateVariables.MaxNumRange);
            storedQuestion2 = Random.Range(0, privateVariables.MaxNumRange);

            // Addition
            if (privateVariables.OperatorType == 0)
            {
                Q1.text = storedQuestion1.ToString();

                Q2.text = storedQuestion2.ToString();

                currentCorrectAnswer = storedQuestion1 + storedQuestion2;
                return;
            }

            // Subtraction
            if (privateVariables.OperatorType == 1)
            {
                if (storedQuestion1 > storedQuestion2)
                {
                    Q1.text = storedQuestion1.ToString();
                    Q2.text = storedQuestion2.ToString();
                    currentCorrectAnswer = storedQuestion1 - storedQuestion2;
                    return;
                }
                else
                {
                    Q1.text = storedQuestion2.ToString();
                    Q2.text = storedQuestion1.ToString();
                    currentCorrectAnswer = storedQuestion2 - storedQuestion1;
                    return;
                }
            }
            if (privateVariables.OperatorType == 2)
            {
                Q1.text = storedQuestion1.ToString();
                Q2.text = storedQuestion2.ToString();
                currentCorrectAnswer = storedQuestion1 * storedQuestion2;
                return;
            }
            if (privateVariables.OperatorType == 3)
            {
                Q1.text = (storedQuestion1 * storedQuestion2).ToString();
                Q2.text = storedQuestion2.ToString();
                currentCorrectAnswer = storedQuestion1;
                return;
            }


        }
        else { Debug.LogError("Random Questions unable to be set"); }
    }

    void OnButtonPress(int buttonNumber)
    {
        Debug.Log($"Button {buttonNumber} pressed");
        if (inputHolder != null) { inputHolder.text = inputHolder.text + buttonNumber; }
        GetCurrentStoredNumber(buttonNumber);
    }
    void OnClearPress()
    {
        if (inputHolder != null) { inputHolder.text = null; }
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
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            globalScoreL = uiDocument.rootVisualElement.Q<Label>("GlobalScore");

            if (globalScoreL != null)
            {
                globalScoreL.text = "Score: " + score;
            }
        }
    }
    public void ShowUI(VisualTreeAsset uiAsset)
    {
        if (uiDocument != null && uiAsset != null)
        {
            uiDocument.rootVisualElement.Clear();  // Clear current UI
            VisualElement newUI = uiAsset.Instantiate();
            newUI.style.flexGrow = 1;
            uiDocument.rootVisualElement.Add(newUI);  // Load new UI
        }
    }
}
