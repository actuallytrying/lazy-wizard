using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; } // Singleton instance

    // TODO: Handle getting pages from current tier (or maybe this actually is total of all pages)
    [SerializeField] List<Page> pages;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep the manager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy additional instances of the manager
        }
    }

    public float CalculateTotalPower()
    {
        float totalRate = 0;
        foreach (Page page in pages)
        {
            foreach (Monster monster in page.monsters)
            {
                totalRate += monster.GetPower();
            }
        }

        return totalRate;
    }

    public List<Monster> GetActiveMonsters()
    {
        List<Monster> activeMonsters = new List<Monster>();
        foreach (Page page in pages)
        {
            foreach (Monster monster in page.monsters)
            {
                if (monster.level > 0)
                {
                    activeMonsters.Add(monster);
                }
            }
        }

        return activeMonsters;
    }

}

