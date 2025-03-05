using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AdditionScript : MonoBehaviour
{
    private UIDocument document;

    private Label Q1;


    // Start is called before the first frame update
    void Start()
    {
        document = GameObject.FindWithTag("Player").GetComponent<UIDocument>();

        Q1 = document.rootVisualElement.Q<Label>("Q1");
        Q1.text = Random.Range(1, 100).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Random.Range(1, 100).ToString());
    }
}
