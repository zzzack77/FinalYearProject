using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    private UIDocument document;

    private Label globalScoreL;
    // Start is called before the first frame update

    void OnEnable()
    {
        document = GetComponent<UIDocument>();

        globalScoreL = document.rootVisualElement.Q<Label>("GlobalScore");

        if (globalScoreL != null)
        {
            globalScoreL.text = "Score: ";
        }
        
    }
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
