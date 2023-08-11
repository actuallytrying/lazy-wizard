using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MonsterSlot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Image icon;
    [SerializeField] Image iconBorder;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] Sprite defaultSprite;

    public Monster currentMonster; 
    public delegate void MonsterHovered(Monster monster);
    public static event MonsterHovered OnMonsterHovered;

    public void SetMonster(Monster monster)
    {
        currentMonster = monster;

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
            iconBorder.color = RarityColors.GetColorFromRarity(monster.rarity);
        } 
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        OnMonsterHovered?.Invoke(currentMonster);
    }
}


