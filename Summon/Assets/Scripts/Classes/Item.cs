using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EffectType
{
    Fire,
    Water,
    Earth,
    Wind,
    DropChance,
    Experience,
    Power
}

public enum EffectApplication
{
    Additive,
    Multiplicative
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject, ICollectible
{
    public string title;
    public Sprite sprite;
    public bool unlocked = false;
    public bool applied = false;
    public float dropChance = 0.01f;
    public List<ItemEffect> effects = new List<ItemEffect>();

    public string GetTitle() => title;
    public Sprite GetImage() => sprite;
    public bool IsUnlocked() => unlocked;

#if UNITY_EDITOR
    // Register the callback when the scriptable object is loaded.
    private void OnEnable()
    {
        UnityEditor.EditorApplication.playModeStateChanged += ResetData;
    }

    // Unregister the callback when the scriptable object is unloaded.
    private void OnDisable()
    {
        UnityEditor.EditorApplication.playModeStateChanged -= ResetData;
    }

    void ResetData(UnityEditor.PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            applied = false;
        }
    }
#endif
}
