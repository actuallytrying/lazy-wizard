using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image iconBorder;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] Sprite defaultSprite;

    public void SetMonster(Monster monster)
    {
        if (monster.level == 0)
        {
            icon.sprite = defaultSprite;
            nameText.text = "???";
        }
        else
        {
            icon.sprite = monster.image;
            nameText.text = monster.title;
            powerText.text = monster.GetPower().ToString();
            SetBorderFromRarity(monster.rarity);
        } 
    }

    public void SetBorderFromRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                iconBorder.color = Color.white;
                break;
            case Rarity.Rare:
                iconBorder.color = Color.blue;
                break;
            case Rarity.Epic:
                iconBorder.color = new Color(0.5f, 0f, 0.5f); 
                break;
            case Rarity.Legendary:
                iconBorder.color = new Color(1f, 0.5f, 0f); 
                break;
        }
    }
}


