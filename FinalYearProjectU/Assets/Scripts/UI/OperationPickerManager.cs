using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class OperationPickerManager : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UILoader uiLoader;
    public UIDocument uiDocument;

    private Button additionB;
    private Button subtractionB;
    private Button multiplicationB;
    private Button divisionB;

    [Header("Addition StartUp Settings")]
    private int maxNumRange0 = 100;
    private float timeRemaining0 = 20;

    [Header("Subtraction StartUp Settings")]
    private int maxNumRange1 = 100;
    private float timeRemaining1 = 20;

    [Header("Multiplication StartUp Settings")]
    private int maxNumRange2 = 12;
    private float timeRemaining2 = 20;

    [Header("Division StartUp Settings")]
    private int maxNumRange3 = 12;
    private float timeRemaining3 = 20;

    private Button backB;

    private void OnEnable()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        OnStartOperatorPicker();
    }

    private void OnStartOperatorPicker()
    {
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;

            additionB = root.Q<Button>("AdditionB");
            subtractionB = root.Q<Button>("SubtractionB");
            multiplicationB = root.Q<Button>("MultiplicationB");
            divisionB = root.Q<Button>("DivisionB");

            additionB.clicked += () => OnAdditionPress();
            subtractionB.clicked += () => OnSubtractionPress();
            multiplicationB.clicked += () => OnMultiplicationPress();
            divisionB.clicked += () => OnDivisionPress();

            backB = root.Q<Button>("BackB");
            if (backB != null) { backB.clicked += OnBackBPress; }
        }
    }

    private void OnAdditionPress()
    {
        privateVariables.TimeRemaining = 20f;
        privateVariables.OperatorType = 0;
        uiLoader.LoadUIByContainerIndex(2);
        this.enabled = false;
    }
    private void OnSubtractionPress()
    {
        privateVariables.TimeRemaining = 20f;
        privateVariables.OperatorType = 1;
        uiLoader.LoadUIByContainerIndex(2);
        this.enabled = false;
    }
    private void OnMultiplicationPress()
    {
        privateVariables.TimeRemaining = 20f;
        privateVariables.OperatorType = 2;
        uiLoader.LoadUIByContainerIndex(2);
        this.enabled = false;
    }
    private void OnDivisionPress()
    {
        privateVariables.TimeRemaining = 20f;
        privateVariables.OperatorType = 3;
        uiLoader.LoadUIByContainerIndex(2);
        this.enabled = false;
    }
    private void OnBackBPress()
    {
        uiLoader.LoadUIByContainerIndex(0);
        this.enabled = false;
    }
    //private void OnAdditionPress() => OnOperationPress(0, maxNumRange0, timeRemaining0, "Addition");
    //private void OnSubtractionPress() => OnOperationPress(1, maxNumRange1, timeRemaining1, "Subtraction");
    //private void OnMultiplicationPress() => OnOperationPress(2, maxNumRange2, timeRemaining2, "Multiplication");
    //private void OnDivisionPress() => OnOperationPress(3, maxNumRange3, timeRemaining3, "Division");

    //private void OnOperationPress(int operatorType, int maxNumRange, float timeRemaining, string operationName)
    //{
    //    Debug.Log($"Pressed {operationName}");
    //    privateVariables.OperatorType = operatorType;
    //    privateVariables.MaxNumRange = maxNumRange;
    //    privateVariables.TimeRemaining = timeRemaining;

    //    uiLoader.LoadUIByContainerIndex(1);
    //    this.enabled = false;
    //}
}
