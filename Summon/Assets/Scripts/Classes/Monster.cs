using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class Monster : ScriptableObject, IFighter, ICollectible
{
    public string title;
    public Sprite image;
    public Rarity rarity;
    public Class monsterClass;
    public Element element;
    public int basePower;
    public int level = 0;
    public int experience = 0;
    public float growthRate = 0.75f;

    private const int baseExperience = 100; // adjust this as needed

    public string Title => title;
    public Sprite Image => image;
    public int Power => GetPower();
    public Rarity Rarity => rarity;
    public Class Class => monsterClass;
    public Element Element => element;


    public string GetTitle() => Title;
    public Sprite GetImage() => Image;
    public bool IsUnlocked() => level > 0;

    public int GetPower()
    {
        float powerMultiplier = Mathf.Max(1, Mathf.Pow(level, growthRate));
        return Mathf.RoundToInt(basePower * powerMultiplier);
    }

    public int GetPowerAtLevel(int level)
    {
        float powerMultiplier = Mathf.Max(1, Mathf.Pow(level, growthRate));
        return Mathf.RoundToInt(basePower * powerMultiplier);
    }

    public int ExperienceForLevel(int level)
    {
        return baseExperience * level * level; // adjust this formula as needed
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= ExperienceForLevel(level + 1))
        {
            experience -= ExperienceForLevel(level + 1);
            level += 1;
        }
    }

    public void AddLevels(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            level += 1;
            if (experience >= ExperienceForLevel(level))
            {
                experience -= ExperienceForLevel(level);
            }
        }
    }

    void OnEnable()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.playModeStateChanged += ResetData;
#endif
    }

#if UNITY_EDITOR
    void ResetData(UnityEditor.PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            level = 0;
            experience = 0;
        }
    }
#endif
}

