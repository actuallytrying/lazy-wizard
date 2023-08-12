using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Page", menuName = "Page")]
public class Page : ScriptableObject
{
    public List<Monster> monsters;

    public List<Monster> GetMonstersByRarity(Rarity rarity)
    {
        return monsters.Where(monster => monster.rarity == rarity).ToList();
    }
}

