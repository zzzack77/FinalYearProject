using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "UIContainer", menuName = "UI/UI Container")]
public class UIContainer : ScriptableObject
{
    public List<VisualTreeAsset> uiAssets = new List<VisualTreeAsset>();
}
