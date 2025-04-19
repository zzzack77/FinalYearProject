using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizCompleteScript : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UILoader uiLoader;
    public UIDocument uiDocument;

    private Button star1;
    private Button star2;
    private Button star3;

    private Label questionsCorrect;

    private Button retryB;
    private Button continueB;

    

    private int xpMultiplier = 10;
    private int goldMultiplier = 5;
    private void OnEnable()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        StartCoroutine(GiveXP());
        StartCoroutine(GiveGold());

        OnQuizCompleteStar();
    }

    private IEnumerator GiveXP()
    {
        for (int i = 0; i < privateVariables.NumberOfCorrectQuestions * xpMultiplier; i++)
        {
            privateVariables.GlobalXP++;
            yield return new WaitForSeconds(0.07f);
        }
    }
    private IEnumerator GiveGold()
    {
        for (int i = 0; i < privateVariables.NumberOfCorrectQuestions * goldMultiplier; i++)
        {
            privateVariables.CurrentGold++;
            yield return new WaitForSeconds(0.14f);
        }
    }
    private void OnQuizCompleteStar()
    {
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;

            star1 = root.Q<Button>("Star1");
            star2 = root.Q<Button>("Star2");
            star3 = root.Q<Button>("Star3");

            questionsCorrect = root.Q<Label>("QuestionsCorrect");
            questionsCorrect.text = privateVariables.NumberOfCorrectQuestions.ToString() + "/" + privateVariables.TotalNumberOfQuestions.ToString() ;

            retryB = root.Q<Button>("RetryB");
            if (retryB != null) retryB.clicked += OnRetryPress;

            continueB = root.Q<Button>("ContinueB");
            if (continueB != null) continueB.clicked += OnContinuePress;
        }
    }
    private void OnRetryPress()
    {
        
        uiLoader.LoadUIByContainerIndex(1);
        this.enabled = false;
    }
    private void OnContinuePress()
    {
        
        uiLoader.LoadUIByContainerIndex(3);
        this.enabled = false;
    }
}
