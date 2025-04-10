using UnityEngine;

[CreateAssetMenu(fileName = "UIImageAsset", menuName = "UI/Image Asset")]
public class UIImageAsset : ScriptableObject
{
    public string key;
    public Texture2D image;
}
