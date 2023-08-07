using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AreaItemsManager : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform itemGrid;

    [SerializeField] TextMeshProUGUI completionEffectText;

    private IEnumerable<Item> areaItems;

    void Start()
    {
        AreaManager.Instance.OnAreaChanged += HandleAreaChange; 
        HandleAreaChange(AreaManager.Instance.CurrentArea); 
    }

    private void OnDestroy()
    {
        AreaManager.Instance.OnAreaChanged -= HandleAreaChange;  // don't forget to unsubscribe!
    }

    private void HandleAreaChange(Area newArea)
    {
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        var allItems = new List<Item>();
        foreach (EnemyTemplate enemy in newArea.enemies)
        {
            allItems.AddRange(enemy.drops);
        }
        areaItems = allItems.Distinct();

        foreach (Item item in areaItems)
        {
            GameObject slot = Instantiate(itemPrefab, itemGrid);
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            itemSlot.SetItem(item);
        }


        string effectString = string.Empty;

        foreach (ItemEffect effect in newArea.itemCompletionEffects)
        {
            effectString += effect.EffectToString() + "\n";
        }

        completionEffectText.text = effectString;

        completionEffectText.color = areaItems.All(item => item.unlocked) ? Color.white : Color.grey;


    }
}
