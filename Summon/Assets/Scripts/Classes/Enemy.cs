using System.Collections.Generic;
using UnityEngine;

public class Enemy : IFighter
{
    public string title;
    public Sprite image;
    public Class enemyClass;
    public Element element;
    public Rarity rarity;
    public int power;

    public List<Item> drops;
    public int experienceDrop;

    public string Title => title;
    public Sprite Image => image;
    public int Power => power;
    public Class Class => enemyClass;
    public Element Element => element;
    public Rarity Rarity => rarity;

    public Enemy(EnemyTemplate template)
    {
        title = template.title;
        image = template.image;
        enemyClass = template.enemyClass;
        power = template.basePower;
        drops = template.drops;
        experienceDrop = template.experienceDrop;

        // Assign random rarity
        float randRarity = Random.Range(0f, 1f);
        if (randRarity < 0.005)
            rarity = Rarity.Legendary;
        else if (randRarity < 0.025)
            rarity = Rarity.Epic;
        else if (randRarity < 0.2)
            rarity = Rarity.Rare;
        else
            rarity = Rarity.Common;

        // Apply rarity multiplier to power
        switch (rarity)
        {
            case Rarity.Common:
                break;  // No bonus
            case Rarity.Rare:
                power = Mathf.RoundToInt(power * 1.2f);
                experienceDrop = Mathf.RoundToInt(experienceDrop * 1.2f);
                break;
            case Rarity.Epic:
                power = Mathf.RoundToInt(power * 1.5f);
                experienceDrop = Mathf.RoundToInt(experienceDrop * 1.5f);
                break;
            case Rarity.Legendary:
                power = Mathf.RoundToInt(power * 2f);
                experienceDrop = Mathf.RoundToInt(experienceDrop * 2.0f);
                break;
        }

        // Assign random element
        element = (Element)Random.Range(0, System.Enum.GetValues(typeof(Element)).Length);
    }

    public int GetPower()
    {
        return power;
    }

    public List<Item> GetDrops()
    {
        List<Item> droppedItems = new List<Item>();
        foreach (Item item in drops)
        {
            if (Random.Range(0f, 1f) <= item.dropChance && !item.unlocked)
            {
                droppedItems.Add(item);
                item.unlocked = true;
            }
        }
        return droppedItems;
    }
}

