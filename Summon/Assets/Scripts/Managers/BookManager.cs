using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour, IScreen
{
    [SerializeField] GameObject generatorSlotPrefab;
    [SerializeField] Transform firstPageParent;
    [SerializeField] Transform secondPageParent;
    [SerializeField] MonsterBook book;

    public void OnActivate()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // TODO: Clean up this code
        // TODO: Fetch current tier from screen manager or whatever

        // First, delete all current generator slots
        foreach (Transform child in firstPageParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in secondPageParent)
        {
            Destroy(child.gameObject);
        }


        Page firstPage = book.tier1Pages[0];
        Page secondPage = book.tier1Pages[1];

        foreach (Monster monster in firstPage.monsters)
        {
            GameObject slot = Instantiate(generatorSlotPrefab, firstPageParent);
            MonsterSlot monsterSlot = slot.GetComponent<MonsterSlot>();
            monsterSlot.SetMonster(monster);
        }

        foreach (Monster monster in secondPage.monsters)
        {
            GameObject slot = Instantiate(generatorSlotPrefab, secondPageParent);
            MonsterSlot monsterSlot = slot.GetComponent<MonsterSlot>();
            monsterSlot.SetMonster(monster);
        }
    }
}


