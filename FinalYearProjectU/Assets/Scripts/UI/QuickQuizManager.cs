using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UIElements;

public class QuickQuizManager : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UILoader uiLoader;
    public UIDocument uiDocument;

    // Pause
    private Button pauseB;
    private VisualElement pauseScreen;

    [SerializeField]
    private bool isGamePaused = false;

    private Button pauseContinueB;
    private Button pauseExitB;


    private Label globalScoreL;

    // Questions and answers
    private Label Operator;

    private Label Q1;
    private Label Q2;
    private int storedQuestion1;
    private int storedQuestion2;

    private VisualElement InputVE;
    private Label inputHolder;

    // Numpad
    private Button[] numPadButtons = new Button[10];

    private Button clearB;
    private Button enterB;

    // To calculate correct answer and store current answer
    [SerializeField]
    private int currentStoredNumber;
    [SerializeField]
    private int currentCorrectAnswer;

    private VisualElement TimerBackground;
    private VisualElement TimerBar;

    [SerializeField]
    private float timeRemaining;
    private float timeStep;

    private bool isInputDestroyable;
    private bool isTimeOn;

    private Label questionsLeft;

    private bool gameEnd;

    void OnEnable()
    {
        gameEnd = false;
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        if (privateVariables != null)
        {
            // If time remaining hasn't been set, revert to defult setings
            if (privateVariables.TimeRemaining == 0)
            {
                privateVariables.OperatorType = 0;
                privateVariables.MaxNumRange = 100;
                privateVariables.TimeRemaining = 20;
                Debug.LogError("Variables have been set to defult");
            }
        }
        else { Debug.LogError("Private variabels didnt assign correctly"); this.enabled = false; }


        OnStartMathsType();
    }



    public void OnStartMathsType()
    {
        privateVariables.NumberOfCorrectQuestions = 0;
        privateVariables.QuestionNumber = 0;
        if (uiDocument != null)
        {
            // Root
            VisualElement root = uiDocument.rootVisualElement;
            Debug.Log(root.name);

            pauseScreen = root.Q<VisualElement>("PauseScreen");
            pauseContinueB = root.Q<Button>("ContinueB");
            pauseExitB = root.Q<Button>("ExitB");
            if (pauseContinueB != null) { pauseContinueB.clicked += TogglePauseScreen; }
            if (pauseExitB != null) { pauseExitB.clicked += OnExitPress; }

            // Initialize score and set empty text
            globalScoreL = root.Q<Label>("GlobalScore");
            if (globalScoreL != null) { globalScoreL.text = "Score: "; }

            pauseB = root.Q<Button>("PauseB");
            if (pauseB != null) { pauseB.clicked += TogglePauseScreen; }

            // Operator handeler
            Operator = root.Q<Label>("QOperator");
            SetOperator();

            // Initilize question labels and set first questions
            Q1 = root.Q<Label>("Q1");
            Q2 = root.Q<Label>("Q2");
            questionsLeft = root.Q<Label>("QuestionsLeft");
            ResetRandomQuestions();

            // Input text holder for number input
            InputVE = root.Q<VisualElement>("InputVE");
            inputHolder = root.Q<Label>("InputLabel");

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

            // Time Code initialization
            TimerBackground = root.Q<VisualElement>("TimerBackground");
            TimerBar = root.Q<VisualElement>("TimerBar");


            StartResetTimeHandeler();
        }
    }
    public void TogglePauseScreen()
    {
        if (pauseScreen != null)
        {
            Debug.Log("pressed continue");

            // if paused resume
            if (isGamePaused)
            {
                isTimeOn = true;
                pauseScreen.visible = false;
                pauseScreen.style.width = Length.Percent(0);
                pauseScreen.style.height = Length.Percent(0);

                isGamePaused = false;
                StartCoroutine(TimerHandeler());


                return;
            }
            else // pause the game
            {
                isTimeOn = false;

                pauseScreen.visible = true;

                pauseScreen.style.width = Length.Percent(100);
                pauseScreen.style.height = Length.Percent(100);
                isGamePaused = true;
                return;
            }
        }
        else Debug.LogError("Pause Screen is Null.");
    }

    private void StartResetTimeHandeler()
    {
        timeRemaining = privateVariables.TimeRemaining;
        isTimeOn = true;
        StartCoroutine(TimerHandeler());
    }
    private void ResetTimeHandeler()
    {
        timeRemaining = privateVariables.TimeRemaining;
        isTimeOn = true;
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
        privateVariables.QuestionNumber++;

        questionsLeft.text = privateVariables.QuestionNumber.ToString() + "/" + privateVariables.TotalNumberOfQuestions.ToString();

        if (privateVariables.QuestionNumber > privateVariables.TotalNumberOfQuestions)
        {
            OnQuizComplete();
        }
        if (isTimeOn) ResetTimeHandeler();
        else StartResetTimeHandeler();

        // Set random questions and store answer for each operator type
        if (Q1 != null && Q2 != null)
        {
            storedQuestion1 = Random.Range(1, privateVariables.MaxNumRange);
            storedQuestion2 = Random.Range(1, privateVariables.MaxNumRange);

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
    void OnExitPress()
    {
        gameEnd = true;
        uiLoader.LoadUIByContainerIndex(3);
        this.enabled = false;
    }
    void OnQuizComplete()
    {
        gameEnd = true;

        uiLoader.LoadUIByContainerIndex(5);
        this.enabled = false;
    }

    void OnButtonPress(int buttonNumber)
    {
        if (isInputDestroyable) OnClearPress();

        // 9999999 is the maximum number you can fit in the text box
        if (currentStoredNumber < 9999999)
        {
            //Debug.Log($"Button {buttonNumber} pressed");
            if (inputHolder != null) { inputHolder.text = inputHolder.text + buttonNumber; }
            GetCurrentStoredNumber(buttonNumber);
        }
    }
    void OnClearPress()
    {
        if (inputHolder != null) { inputHolder.text = null; }
        currentStoredNumber = 0;
        isInputDestroyable = false;
    }
    void OnEnterPress() { CheckAnswer(); }
    void CheckAnswer()
    {
        if (currentCorrectAnswer == currentStoredNumber) StartCoroutine(CorrectAnswer());
        else { StartCoroutine(WrongAnswer()); }
    }
    void GetCurrentStoredNumber(int value) { currentStoredNumber = (currentStoredNumber * 10) + value; }

    public void UpdateScore(int score)
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            globalScoreL = uiDocument.rootVisualElement.Q<Label>("GlobalScore");

            if (globalScoreL != null) globalScoreL.text = "Score: " + score;
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
    private IEnumerator WrongAnswer()
    {
        //StopCoroutine(TimerHandeler());
        isInputDestroyable = true;
        if (InputVE != null)
        {
            for (int i = 0; i < 2; i++)
            {
                if (isInputDestroyable)
                {
                    InputVE.AddToClassList("VEWrongAnswer");
                    yield return new WaitForSeconds(.2f);
                    InputVE.RemoveFromClassList("VEWrongAnswer");
                    yield return new WaitForSeconds(.2f);
                }
                else yield break;
            }

            if (isInputDestroyable) OnClearPress();
        }
    }
    private IEnumerator CorrectAnswer()
    {
        privateVariables.NumberOfCorrectQuestions++;
        isTimeOn = false;
        StopCoroutine(TimerHandeler());

        // Flash Green
        isInputDestroyable = true;
        if (InputVE != null)
        {
            for (int i = 0; i < 2; i++)
            {
                if (isInputDestroyable)
                {
                    InputVE.AddToClassList("VECorrectAnswer");
                    yield return new WaitForSeconds(.2f);
                    InputVE.RemoveFromClassList("VECorrectAnswer");
                    yield return new WaitForSeconds(.2f);
                }
                else
                {
                    ResetRandomQuestions();
                    yield break;
                }
            }
            ResetRandomQuestions();
            if (isInputDestroyable) OnClearPress();
        }
    }

    private IEnumerator TimerHandeler()
    {
        if (TimerBackground != null && TimerBar != null)
        {
            while (timeRemaining > 0 && isTimeOn && !gameEnd)
            {
                TimerBar.style.width = Length.Percent((timeRemaining / privateVariables.TimeRemaining) * 100);
                yield return new WaitForSeconds(privateVariables.TimeRemaining / 2000f);
                timeRemaining -= privateVariables.TimeRemaining / 2000f;
            }

            if (timeRemaining < 0)
            {
                TimerBackground.AddToClassList("VEWrongAnswer");
                yield return new WaitForSeconds(.2f);
                TimerBackground.RemoveFromClassList("VEWrongAnswer");
                yield return new WaitForSeconds(.2f);
                TimerBackground.AddToClassList("VEWrongAnswer");
                yield return new WaitForSeconds(.2f);
                TimerBackground.RemoveFromClassList("VEWrongAnswer");
                ResetRandomQuestions();
                StartResetTimeHandeler();
            }
        }
    }
}
