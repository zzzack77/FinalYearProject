using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class UILoader : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private QuickQuizManager quickQuizManager;
    private SettingsPickerManager settingsPickerManager;
    private OperationPickerManager operationPickerManager;
    private MenuManager menuManager;
    private PetPickerScript petPickerScript;
    public UIDocument uiDocument;
    public UIContainer uiContainer;

    public int instantLoad;
    // Start is called before the first frame update
    private void Awake()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        quickQuizManager = gameObject.GetComponent<QuickQuizManager>();
        settingsPickerManager = gameObject.GetComponent<SettingsPickerManager>();
        operationPickerManager = gameObject.GetComponent<OperationPickerManager>();
        menuManager = gameObject.GetComponent<MenuManager>();
        petPickerScript = gameObject.GetComponent<PetPickerScript>();
        uiDocument = GetComponent<UIDocument>();
    }
    void Start()
    {
        if (instantLoad == -1) LoadUIByContainerIndex(0);
        else LoadUIByContainerIndex(instantLoad);
    }

    public void LoadUIByContainerIndex(int containerIndex)
    {
        if (uiContainer.uiAssets.Count > 0)
        {
            if (uiDocument != null)
            {
                VisualTreeAsset uiAsset = uiContainer.uiAssets[containerIndex];
                if (uiAsset != null)
                {
                    uiDocument.rootVisualElement.Clear();
                    VisualElement newUI = uiAsset.Instantiate();
                    newUI.style.flexGrow = 1;
                    uiDocument.rootVisualElement.Add(newUI);
                    Debug.Log("Loading UI: " + uiAsset.name);
                    EnableScriptAtachedToUI(uiAsset.name);
                    UpdateMenuValues();
                    
                }
                else Debug.LogError("Failed to load new UI; uiAsset == null");
            }
        }
        else Debug.LogError("Failed to load new UI, container is empty.");
    }
    private void EnableScriptAtachedToUI(string uiName)
    {
        if (uiName == "QuickQuizUI") quickQuizManager.enabled = true;
        if (uiName == "QuickQuizSettingsUI") settingsPickerManager.enabled = true;
        if (uiName == "OperatorPickerUI") operationPickerManager.enabled = true;
        if (uiName == "PetPickerUI")
        {
            DisableScripts();
            petPickerScript.enabled = true;
        }
        //if (uiName == "MainMenu") operationPickerManager.enabled = true;
        //else Debug.LogError("Failed to enable QuickQuizUI's script");
    }
    private void DisableScripts()
    {
        quickQuizManager.enabled = false;
        settingsPickerManager.enabled = false;
        operationPickerManager.enabled = false;
        petPickerScript.enabled = false;
    }
    private void UpdateMenuValues()
    {
        menuManager.UpdateGold();
        menuManager.UpdateLevel();
        menuManager.UpdatePet(privateVariables.CurrentPet);
    }
}
