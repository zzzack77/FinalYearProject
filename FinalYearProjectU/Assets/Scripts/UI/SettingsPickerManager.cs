using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPickerManager : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UILoader uiLoader;
    public UIDocument uiDocument;

    private Button setting1;
    private Button setting2;
    private Button setting3;
    private Button setting4;

    [Header("Addition Settings")]
    private string B1T0 = "1 - 10";
    private int B1V0 = 10;

    private string B2T0 = "1 - 25";
    private int B2V0 = 25;

    private string B3T0 = "1 - 50";
    private int B3V0 = 50;

    private string B4T0 = "1 - 100";
    private int B4V0 = 100;

    [Header("Subtraction Settings")]
    private string B1T1 = "1 - 10";
    private int B1V1 = 10;

    private string B2T1 = "1 - 25";
    private int B2V1 = 25;

    private string B3T1 = "1 - 50";
    private int B3V1 = 50;

    private string B4T1 = "1 - 100";
    private int B4V1 = 100;

    [Header("Multiplication Settings")]
    private string B1T2 = "1 - 4";
    private int B1V2 = 4;

    private string B2T2 = "1 - 8";
    private int B2V2 = 8;

    private string B3T2 = "1 - 10";
    private int B3V2 = 10;

    private string B4T2 = "1 - 12";
    private int B4V2 = 12;

    [Header("Division Settings")]
    private string B1T3 = "1 - 4";
    private int B1V3 = 4;

    private string B2T3 = "1 - 8";
    private int B2V3 = 8 ;

    private string B3T3 = "1 - 10";
    private int B3V3 = 10;

    private string B4T3 = "1 - 12";
    private int B4V3 = 12;

    string[] titles = new string[4];
    int[] values = new int[4];

    void OnEnable()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        OnSettingsStart();
    }

    private void OnSettingsStart()
    {
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;

            setting1 = root.Q<Button>("Setting1");
            setting2 = root.Q<Button>("Setting2");
            setting3 = root.Q<Button>("Setting3");
            setting4 = root.Q<Button>("Setting4");

            if (setting1 != null) { setting1.clicked += OnSetting1Press; }
            if (setting2 != null) { setting2.clicked += OnSetting2Press; }
            if (setting3 != null) { setting3.clicked += OnSetting3Press; }
            if (setting4 != null) { setting4.clicked += OnSetting4Press; }

            int op = privateVariables.OperatorType;

            
            switch (op)
            {
                case 0: // Addition
                    titles = new string[] { B1T0, B2T0, B3T0, B4T0 };
                    values = new int[] { B1V0, B2V0, B3V0, B4V0 };
                    break;
                case 1: // Subtraction
                    titles = new string[] { B1T1, B2T1, B3T1, B4T1 };
                    values = new int[] { B1V1, B2V1, B3V1, B4V1 };
                    break;
                case 2: // Multiplication
                    titles = new string[] { B1T2, B2T2, B3T2, B4T2 };
                    values = new int[] { B1V2, B2V2, B3V2, B4V2 };
                    break;
                case 3: // Division
                    titles = new string[] { B1T3, B2T3, B3T3, B4T3 };
                    values = new int[] { B1V3, B2V3, B3V3, B4V3 };
                    break;
            }

            // Set text on all buttons
            setting1.text = titles[0];
            setting2.text = titles[1];
            setting3.text = titles[2];
            setting4.text = titles[3];
        }

    }
    
    private void OnSetting1Press() { LoadUI(0); }
    private void OnSetting2Press() { LoadUI(1); }
    private void OnSetting3Press() { LoadUI(2); }
    private void OnSetting4Press() { LoadUI(3); }

    private void LoadUI(int i)
    {
        privateVariables.MaxNumRange = values[i];
        uiLoader.LoadUIByContainerIndex(1);
        this.enabled = false;
    }
}
