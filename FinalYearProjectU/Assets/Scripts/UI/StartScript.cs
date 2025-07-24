using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartScript : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private PlayerDataManager playerDataManager;
    private UILoader uiLoader;
    public UIDocument uiDocument;


    private Button startB;
    private Button quitB;
    private void OnEnable()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();
        playerDataManager = gameObject.GetComponent<PlayerDataManager>();

        playerDataManager.SetPlayerName("Gamified");

        OnStartScript();
        StartCoroutine(CountSeconds());

    }

    // Adds 1 each second to record how long the user has been playing in seconds
    IEnumerator CountSeconds()
    {
        yield return new WaitForSeconds(1f);
        playerDataManager.SetTotalTimePlayed(playerDataManager.GetTotalTimePlayed() + 1);
        StartCoroutine(CountSeconds());
    }

    private void OnStartScript()
    {
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;

            if (root != null)
            {
                startB = root.Q<Button>("Play");
                quitB = root.Q<Button>("Quit");
                if (startB != null) startB.clicked += OnStartPress;
                if (quitB != null) quitB.clicked += OnQuitPress;
            }
        }
    }
    private void OnStartPress()
    {
        uiLoader.LoadUIByContainerIndex(3);
        this.enabled = false;
    }
    private void OnQuitPress()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
