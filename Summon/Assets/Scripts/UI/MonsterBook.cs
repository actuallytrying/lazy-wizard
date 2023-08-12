using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterBook : MonoBehaviour
{
    [SerializeField]
    public List<Page> tier1Pages;

    [SerializeField]
    public List<Page> tier2Pages;

    // Assuming each tier has exactly 2 pages
    public List<Page> GetPagesByTier(Tier tier)
    {
        List<Page> tierPages;

        switch (tier)
        {
            case Tier.First:
                tierPages = tier1Pages;
                break;
            case Tier.Second:
                tierPages = tier2Pages;
                break;
            default:
                return null; // or handle error as needed
        }

        float percentageUnlocked = CalculateUnlockedPercentage(tierPages[0]);
        if (percentageUnlocked >= 0.9f)
        {
            return tierPages; // Return all pages in the tier
        }
        else
        {
            return new List<Page> { tierPages[0] }; // Return only the first page in the tier
        }
    }

    private float CalculateUnlockedPercentage(Page page)
    {
        int totalMonsters = page.monsters.Count;
        int unlockedMonsters = page.monsters.Count(monster => monster.level > 0);

        return totalMonsters > 0 ? (float)unlockedMonsters / totalMonsters : 0;
    }
}

