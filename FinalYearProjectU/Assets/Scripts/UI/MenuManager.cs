using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UILoader uiLoader;
    public UIDocument uiDocument;
    public UIImageLibrarySO imageLibary;

    private VisualElement currentLevelVE;
    public VisualElement root;

    private Label levelText;
    private VisualElement levelBar;
    private Label levelRemaining;

    private Label goldText;

    private VisualElement currentPet;
    private Button petB;
    private Button shopB;

    void Start()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        //UpdateLevelAndPet();
    }
    public bool IfMenuIsTrue()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            root = uiDocument.rootVisualElement;

            currentLevelVE = root.Q<VisualElement>("CurrentLevelVE");
            if (currentLevelVE != null)
            {
                petB = root.Q<Button>("PetB");
                if (petB != null) petB.clicked += OnPetPress;

                shopB = root.Q<Button>("SettingsB");
                if (shopB != null) shopB.clicked += OnPetPress;

                return true;
            }
        }
        return false;
    }

    public bool IfPetIsTrue()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            root = uiDocument.rootVisualElement;

            currentLevelVE = root.Q<VisualElement>("FooterAnimalVE");
            if (currentLevelVE != null)
            {
                petB = root.Q<Button>("PetB");
                if (petB != null) petB.clicked += OnPetPress;

                return true;
            }

        }
        return false;
    }
    
    public void UpdateGold()
    {
        if (IfMenuIsTrue())
        {
            goldText = root.Q<Label>("GoldText");
            goldText.text = "Gold " + privateVariables.CurrentGold.ToString();
        }
    }
    
    public void UpdateLevel()
    {
        if (IfMenuIsTrue()) 
        {
            levelText = root.Q<Label>("LevelText");

            if (levelText != null && privateVariables != null)
            {
                levelText.text = "Level " + privateVariables.CurrentLevel.ToString();
            }
            else Debug.LogError("levelText is null");

            levelBar = root.Q<VisualElement>("LevelBar");
            if (levelBar != null) levelBar.style.width = Length.Percent(privateVariables.CalculatePercentageXP());
            levelRemaining = root.Q<Label>("LevelRemaining");
            if (levelRemaining != null) levelRemaining.text = privateVariables.CalculateRemainingXP();
        }
    }
    public void UpdatePet(string imageKey)
    {
        if (IfPetIsTrue())
        {
            currentPet = root.Q<VisualElement>("CurrentPet");
            if (imageLibary.GetImage(imageKey) != null)
            {
                Texture2D image = imageLibary.GetImage(imageKey);
                if (image != null)
                {
                    currentPet.style.backgroundImage = new StyleBackground(image);
                }
                else currentPet.style.backgroundImage = new StyleBackground(new Texture2D(0, 0));
            }
        }
    }
    
    private void OnPetPress()
    {
        uiLoader.LoadUIByContainerIndex(4);
    }
}
