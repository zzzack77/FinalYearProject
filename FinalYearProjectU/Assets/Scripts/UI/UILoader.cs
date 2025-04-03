using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class UILoader : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private QuickQuizManager quickQuizManager;

    public UIDocument uiDocument;
    public UIContainer uiContainer;

    public int instantLoad;
    // Start is called before the first frame update
    private void Awake()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        quickQuizManager = gameObject.GetComponent<QuickQuizManager>();
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
                }
                else Debug.LogError("Failed to load new UI; uiAsset == null");
            }
        }
        else Debug.LogError("Failed to load new UI, container is empty.");
    }
    private void EnableScriptAtachedToUI(string uiName)
    {
        if (uiName == "QuickQuizUI") quickQuizManager.enabled = true;
        //else Debug.LogError("Failed to enable QuickQuizUI's script");
    }
}
