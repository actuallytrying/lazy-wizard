using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private MonsterBook book;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        book = GetComponent<MonsterBook>();
    }

    public int GetTotalCombatPower()
    {
        int total = 0;

        // List to store all pages across all tiers
        List<Page> allPages = new List<Page>();

        // Concatenate all tier pages into one list
        allPages.AddRange(book.tier1Pages);
        allPages.AddRange(book.tier2Pages);
        //allPages.AddRange(book.tier3Pages);
        //allPages.AddRange(book.tier4Pages);
        //allPages.AddRange(book.tier5Pages);

        // Iterate through all pages
        foreach (Page page in allPages)
        {
            // Sum up the power of all monsters in each page
            foreach (Monster monster in page.monsters)
            {
                total += monster.GetPower();
            }
        }

        return total;
    }

}

