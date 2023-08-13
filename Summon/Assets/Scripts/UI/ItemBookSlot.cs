using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemBookSlot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Sprite defaultSprite;

    public Item currentItem; 
    public delegate void ItemHovered(Item item);
    public static event ItemHovered OnItemHovered;

    public void SetItem(Item item)
    {
        currentItem = item;

        if (!item.unlocked)
        {
            icon.sprite = defaultSprite;
            nameText.text = "???";
        }
        else
        {
            icon.sprite = item.sprite;
            nameText.text = item.title;
        } 
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        OnItemHovered?.Invoke(currentItem);
    }
}

