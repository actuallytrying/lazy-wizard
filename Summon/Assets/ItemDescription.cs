using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI dropChanceText;
    [SerializeField] TextMeshProUGUI effectsText; // Single TextMeshProUGUI to display all effects.

    public void SetItem(Item item)
    {
        GetComponent<CanvasGroup>().alpha = 1;

        // Setting icon and title
        itemIcon.sprite = item.sprite;
        titleText.text = item.title;

        // Setting drop chance
        dropChanceText.text = $"Drop Chance: {item.dropChance * 100f}%";

        // Displaying item effects
        effectsText.text = ""; // Reset the effects text first.
        foreach (ItemEffect effect in item.effects)
        {
            effectsText.text += effect.EffectToString() + "\n";
        }
    }

    void OnEnable()
    {
        // Assuming you have a similar event like OnMonsterHovered for items.
        ItemBookSlot.OnItemHovered += SetItem;
    }

    void OnDisable()
    {
        // Assuming you have a similar event like OnMonsterHovered for items.
        ItemBookSlot.OnItemHovered -= SetItem;
    }
}
