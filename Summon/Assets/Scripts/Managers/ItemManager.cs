using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    public List<Item> Items { get; private set; }

    private void Awake()
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
    }

    private void Start()
    {
        PopulateItems();
        ApplyItemEffects();
    }

    public void OnEnemyDefeat(Enemy enemy)
    {
        var items = enemy.drops;
        bool newItemUnlocked = false;
        if (items == null || items.Count == 0) return;

        foreach (Item item in items)
        {
            if (item.unlocked) continue;

            // generate a random number between 0 and 1
            float roll = Random.Range(0.0f, 1.0f);

            // if the random roll is less than or equal to the item's drop chance, unlock it
            if (roll <= item.dropChance)
            {
                Debug.Log("Item: " + item.title + " unlocked!");
                item.unlocked = true;
                newItemUnlocked = true;
            }
        }

        if (newItemUnlocked)
        {
            ApplyItemEffects();
        }
    }

    void PopulateItems()
    {
        Items = new List<Item>();
        string folderPath = "Assets/Objects/Items";  // Replace with your folder path

        // Get file paths of all ScriptableObject files in the folder
        string[] filePaths = Directory.GetFiles(folderPath, "*.asset");

        // Load each file as a ScriptableObject and add to the list
        foreach (string filePath in filePaths)
        {
            Item item = AssetDatabase.LoadAssetAtPath<Item>(filePath);
            if (item != null)
            {
                Items.Add(item);
            }
        }

        Debug.Log("Loaded " + Items.Count + " items.");
    }

    public void ApplyItemEffects()
    {
        foreach (Item item in Items)
        {
            if (item.unlocked && !item.applied)
            {
                foreach (ItemEffect effect in item.effects)
                {
                    effect.ApplyEffect();
                }
                item.applied = true;
            }
        }
    }
}
