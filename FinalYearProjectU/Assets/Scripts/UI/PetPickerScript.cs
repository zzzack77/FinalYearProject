using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PetPickerScript : MonoBehaviour
{
    private PrivateVariables privateVariables;
    private UILoader uiLoader;
    public UIDocument uiDocument;
    public UIImageLibrarySO imageLibary;

    private Button backB;

    private Button[] petButtons = new Button[8];

    private VisualElement areYouSureVE;
    private Button yes;
    private Button no;

    private int storedPetForUnlock;
    
    private void OnEnable()
    {
        privateVariables = gameObject.GetComponent<PrivateVariables>();
        uiLoader = gameObject.GetComponent<UILoader>();
        uiDocument = GetComponent<UIDocument>();

        OnPetPickerStart();
    }
    private void OnPetPickerStart()
    {
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;

            backB = root.Q<Button>("BackB");
            if (backB != null) backB.clicked += OnBackPress;

            areYouSureVE = root.Q<VisualElement>("AreYouSure");
            yes = root.Q<Button>("YesB");
            no = root.Q<Button>("NoB");
            if (yes != null) yes.clicked += OnYesPress;
            if (no != null) no.clicked += OnNoPress;

            for (int i = 0; i < 8; i++)
            {
                petButtons[i] = root.Q<Button>($"Pet{i + 1}");
                if (petButtons[i] != null)
                {
                    petButtons[i].style.backgroundImage = new StyleBackground(imageLibary.GetImageByIndex(i));

                    int index = i;
                    petButtons[i].clicked += () => PressButton(index);
                }
            }
            UpdatePetButtonStyle();
        }
    }
   
    private void PressButton(int i)
    {
        if (privateVariables.UnlockedPets[i] == false)
        {
            AreYouSure(i);
        }
        if (privateVariables.UnlockedPets[i] == true)
        {
            privateVariables.CurrentPet = imageLibary.GetImageKeyByIndex(i);
        }
        
        UpdatePetButtonStyle();


        petButtons[i].style.backgroundImage = new StyleBackground(imageLibary.GetImageByIndex(i));
    }
    private void UpdatePetButtonStyle()
    {
        for (int i = 0; i < 8; i++)
        {
            if (petButtons[i] != null)
            {
                if (privateVariables.UnlockedPets[i] == false)
                {
                    petButtons[i].style.unityBackgroundImageTintColor = new StyleColor(new Color32(50, 50, 50, 255));
                }
                if (privateVariables.UnlockedPets[i] == true)
                {
                    petButtons[i].style.unityBackgroundImageTintColor = new StyleColor(new Color32(255, 255, 255, 255));
                }
            }
            else Debug.LogError("Pet buttons did not load");

            if (privateVariables.CurrentPet == imageLibary.GetImageKeyByIndex(i)) petButtons[i].style.backgroundColor = new StyleColor(new Color32(183, 183, 183, 255));
            else petButtons[i].style.backgroundColor = new StyleColor(new Color32(183, 183, 183, 99));
        }
    }

    private void AreYouSure(int i)
    {
        storedPetForUnlock = i;
        if (areYouSureVE != null)
        {
            areYouSureVE.visible = true;
            areYouSureVE.style.width = Length.Percent(100);
            areYouSureVE.style.height = Length.Percent(100);
        }
    }
    private void OnYesPress()
    {
        privateVariables.CurrentPet = imageLibary.GetImageKeyByIndex(storedPetForUnlock);
        privateVariables.UnlockPet(storedPetForUnlock);
        UpdatePetButtonStyle();
        if (areYouSureVE != null)
        {
            areYouSureVE.visible = false;
            areYouSureVE.style.width = Length.Percent(0);
            areYouSureVE.style.height = Length.Percent(0);
        }
    }
    private void OnNoPress()
    {
        if (areYouSureVE != null)
        {
            areYouSureVE.visible = false;
            areYouSureVE.style.width = Length.Percent(0);
            areYouSureVE.style.height = Length.Percent(0);
        }
    }
    private void OnBackPress()
    {
        uiLoader.LoadUIByContainerIndex(3);
        this.enabled = false;
    }
}
