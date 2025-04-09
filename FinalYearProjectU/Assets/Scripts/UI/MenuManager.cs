using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UILoader uiLoader;
    public UIDocument uiDocument;

    private VisualElement currentLevelVE;

    private Label levelText;
    private Label goldText;

    private VisualElement levelBar;
    private Label levelRemaining;

    // Start is called before the first frame update
    void Start()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        //UpdateLevelAndPet();
    }

    public void UpdateLevel()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;

            currentLevelVE = root.Q<VisualElement>("CurrentLevelVE");
            if (currentLevelVE != null)
            {

                levelText = root.Q<Label>("LevelText");
                if (levelText != null) levelText.text = privateVariables.CurrentLevel.ToString();
                else Debug.LogError("levelText is null");

                goldText = root.Q<Label>("GoldText");

                levelBar = root.Q<VisualElement>("LevelBar");
                levelBar.style.width = Length.Percent(privateVariables.CalculatePercentageXP());
                //Debug.Log(privateVariables.CalculatePercentageXP());
                levelRemaining = root.Q<Label>("LevelRemaining");
                levelRemaining.text = privateVariables.CalculateRemainingXP();
            }
        }
    }

}
