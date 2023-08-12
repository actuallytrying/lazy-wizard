using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image icon;

    public void SetItem(Item item)
    {
        if (item.unlocked)
        {
            icon.sprite = item.sprite;
            icon.color = Color.white;
        } else
        {
            icon.color = Color.black;
        }
    }
}
