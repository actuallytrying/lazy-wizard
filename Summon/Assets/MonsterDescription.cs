using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDescription : MonoBehaviour
{
    [SerializeField] Image monsterIcon;
    [SerializeField] Image monsterIconBorder;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI combatPowerText;
    [SerializeField] Sprite defaultSprite;

    [Header("Level UI")]
    [SerializeField] CanvasGroup levelUICG;
    [SerializeField] Image experienceFiller; // Experience filler bar
    [SerializeField] TextMeshProUGUI levelBadgeText; // Text inside the badge for the current level
    [SerializeField] TextMeshProUGUI experienceText; // Text below the bar showing "{current exp / total exp}"

    public void SetMonster(Monster monster) {
        GetComponent<CanvasGroup>().alpha = 1;

        if (monster.level == 0)
        {
            levelUICG.alpha = 0;
            monsterIcon.sprite = defaultSprite;
            titleText.text = "???";
        }
        else
        {
            monsterIcon.sprite = monster.image;
            monsterIconBorder.color = RarityColors.GetColorFromRarity(monster.rarity);
            titleText.text = monster.title;
            combatPowerText.text = "CP: " + monster.Power;
            // Level UI
            UpdateExperienceUI(monster);
        }
    }

    private void UpdateExperienceUI(Monster monster)
    {
        levelUICG.alpha = 1;
        float experienceFraction = (float)monster.experience / (float)monster.ExperienceForLevel(monster.level + 1);
        experienceFiller.fillAmount = experienceFraction;

        levelBadgeText.text = monster.level.ToString();
        experienceText.text = $"{monster.experience} / {monster.ExperienceForLevel(monster.level + 1)}";
    }

    void OnEnable()
    {
        MonsterSlot.OnMonsterHovered += SetMonster; // Subscribe to event
    }
    
    void OnDisable()
    {
        MonsterSlot.OnMonsterHovered -= SetMonster; // Unsubscribe from event
    }
}
