using UnityEngine;

public static class RarityColors
{
    public static readonly Color Common = new Color(0.5f, 0.5f, 0.5f, 1.0f); // Grey
    public static readonly Color Rare = new Color(0.0f, 0.7f, 1.0f, 1.0f); // Light Blue
    public static readonly Color Epic = new Color(0.5f, 0.0f, 0.5f, 1.0f); // Purple
    public static readonly Color Legendary = new Color(1.0f, 0.5f, 0.0f, 1.0f); // Orange

    public static Color GetColorFromRarity(Rarity rarity) {
        return rarity switch
        {
            Rarity.Common => Common,
            Rarity.Rare => Rare,
            Rarity.Epic => Epic,
            Rarity.Legendary => Legendary,
            _ => Common,
        };
    }
}

