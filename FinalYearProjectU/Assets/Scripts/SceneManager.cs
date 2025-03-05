using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerOne : MonoBehaviour
{
    private PrivateVariables privateVariables;
    public static SceneManagerOne Instance;

    private void Awake()
    {
        privateVariables = GetComponent<PrivateVariables>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Level1.0")
            {
                SceneManager.LoadScene("Level2.0");

            }
            else
            {
                SceneManager.LoadScene("Level1.0");

            }

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            privateVariables.GlobalScore++;
        }
    }
}
