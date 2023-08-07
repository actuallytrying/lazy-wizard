using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemEffect
{
    public EffectType type;
    public float value;
    public EffectApplication application = EffectApplication.Additive;

    public void ApplyEffect()
    {
        switch (type)
        {
            case EffectType.Fire:
                ApplyBonus(ref StatsManager.Instance.fireBonus);
                break;
            case EffectType.Water:
                ApplyBonus(ref StatsManager.Instance.waterBonus);
                break;
            case EffectType.Earth:
                ApplyBonus(ref StatsManager.Instance.earthBonus);
                break;
            case EffectType.Wind:
                ApplyBonus(ref StatsManager.Instance.windBonus);
                break;
            case EffectType.DropChance:
                ApplyBonus(ref StatsManager.Instance.dropChanceBonus);
                break;
            case EffectType.Experience:
                ApplyBonus(ref StatsManager.Instance.experienceBonus);
                break;
            case EffectType.Power:
                ApplyBonus(ref StatsManager.Instance.powerBonus);
                break;
        }
    }

    public string EffectToString()
    {
        string appSymbol = "";
        string effectType = type.ToString();
        string valueAsString = "";

        // Convert value to appropriate format
        if (application == EffectApplication.Additive)
        {
            appSymbol = "+";
            valueAsString = $"{value * 100}%"; // Converts to percentage
        }
        else if (application == EffectApplication.Multiplicative)
        {
            appSymbol = "x";
            valueAsString = value.ToString(); // Keeps raw value
        }

        // This switch-case can be used to customize the string representation of each EffectType.
        switch (type)
        {
            case EffectType.Fire:
                effectType = "Fire Damage";
                break;
            case EffectType.Water:
                effectType = "Water Resistance";
                break;
            case EffectType.Earth:
                effectType = "Earth Armor";
                break;
            case EffectType.Wind:
                effectType = "Wind Speed";
                break;
            case EffectType.DropChance:
                effectType = "Item Drop Chance";
                break;
            case EffectType.Experience:
                effectType = "Experience Gain";
                break;
            case EffectType.Power:
                effectType = "Power";
                break;
        }

        return $"{appSymbol} {valueAsString} to {effectType}";
    }


    private void ApplyBonus(ref float bonus)
    {
        if (application == EffectApplication.Additive)
        {
            bonus += value;
        }
        else if (application == EffectApplication.Multiplicative)
        {
            bonus *= value;
        }
    }
}
