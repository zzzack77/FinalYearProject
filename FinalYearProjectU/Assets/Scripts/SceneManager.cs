using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerOne : MonoBehaviour
{
    public static SceneManagerOne Instance;

    private void Awake()
    {
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

    public void LoadScene(string sceneName)
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
