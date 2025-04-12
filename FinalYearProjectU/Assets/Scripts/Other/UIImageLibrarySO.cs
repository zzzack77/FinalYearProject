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
    public Texture2D GetImageByIndex(int index)
    {
        if (images == null || index < 0 || index >= images.Count)
        {
            Debug.LogWarning($"Invalid image index: {index}. Must be between 0 and {images.Count - 1}.");
            return null;
        }

        return images[index].image;
    }
    
    public string GetImageKeyByIndex(int index)
    {
        if (images == null || index < 0 || index >= images.Count)
        {
            Debug.LogWarning($"Invalid image index: {index}. Must be between 0 and {images.Count - 1}.");
            return null;
        }

        return images[index].key;
    }
}
