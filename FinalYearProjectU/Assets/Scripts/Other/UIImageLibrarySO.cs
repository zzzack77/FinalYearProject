using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIImageLibrary", menuName = "UI/Image Library")]
public class UIImageLibrarySO : ScriptableObject
{
    public List<UIImageAsset> images;

    private Dictionary<string, Texture2D> imageDict;

    public void Initialize()
    {
        if (imageDict == null)
        {
            imageDict = new Dictionary<string, Texture2D>();
            foreach (var img in images)
            {
                if (!imageDict.ContainsKey(img.key))
                    imageDict.Add(img.key, img.image);
            }
        }
    }

    public Texture2D GetImage(string key)
    {
        Initialize();
        if (imageDict.TryGetValue(key, out var tex))
            return tex;

        Debug.LogWarning($"Image with key '{key}' not found.");
        return null;
    }
}
