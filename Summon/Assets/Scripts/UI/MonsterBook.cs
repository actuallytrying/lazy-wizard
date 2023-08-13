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

    
    public List<Page> GetPages() {
        return tier1Pages.Concat(tier2Pages).ToList();
    }
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

        return tierPages; 
    }
}

