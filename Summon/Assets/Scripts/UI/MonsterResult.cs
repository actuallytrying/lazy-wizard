using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class MonsterResult : MonoBehaviour
{
    [SerializeField] float fadeAwaySeconds = 30.0f;
    [SerializeField] Image icon;
    [SerializeField] Image iconBorder;

    [SerializeField] Image levelsGainedBackground;

    private CanvasGroup canvasGroup;
    [SerializeField] private AnimatedCounter combatPowerCounter;
    [SerializeField] private AnimatedCounter levelCounter;

    private Monster currentMonster;
    private int lastLevelsGained;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
    }

    public void SetMonster(Monster monster, int levelsGained)
    {
        currentMonster = monster;
        lastLevelsGained = levelsGained;
        iconBorder.color = Color.black; 
        icon.color = Color.black;
    }

    public void RevealMonster()
    {
        // Call animations
        icon.color = Color.white;
        icon.sprite = currentMonster.image;
        AnimateChanges(currentMonster, lastLevelsGained);
        SetBorderFromRarity(currentMonster.rarity);
        SetLevelsGainedBorderFromLevels(lastLevelsGained);
    }

    public void AnimateCombatPowerChange(Monster monster, int levelsGained)
    {
        combatPowerCounter.SetNumber(monster.GetPowerAtLevel(monster.level - levelsGained), monster.GetPower());
    }

    public void AnimateLevelChange(Monster monster, int levelsGained)
    {
        levelCounter.SetNumber(monster.level - levelsGained, monster.level);
    }

    public void SetBorderFromRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                iconBorder.color = RarityColors.Common;
                break;
            case Rarity.Rare:
                iconBorder.color = RarityColors.Rare;
                break;
            case Rarity.Epic:
                iconBorder.color = RarityColors.Epic;
                break;
            case Rarity.Legendary:
                iconBorder.color = RarityColors.Legendary;
                break;
        }
    }

    public void SetLevelsGainedBorderFromLevels(int levelsGained)
    {
        switch (levelsGained)
        {
            case 1:
                levelsGainedBackground.color = RarityColors.Common;
                break;
            case 2:
                levelsGainedBackground.color = RarityColors.Rare;
                break;
            case 3:
                levelsGainedBackground.color = RarityColors.Epic;
                break;
            case 4:
                levelsGainedBackground.color = RarityColors.Legendary;
                break;
            default:
                Debug.LogWarning("Invalid levels gained: " + levelsGained);
                break;
        }
    }

    public void AnimateChanges(Monster monster, int levelsGained)
    {
        AnimateCombatPowerChange(monster, levelsGained);
        AnimateLevelChange(monster, levelsGained);
    }

    public IEnumerator FadeAndDestroyRoutine()
    {
        RevealMonster();

        // Fade in
        float fadeDuration = 0.25f;
        float startTime = Time.time;
        while (Time.time - startTime < fadeDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeDuration;
            canvasGroup.alpha = normalizedTime;
            yield return null;
        }

        // Wait for X seconds
        yield return new WaitForSeconds(fadeAwaySeconds);

        // Fade out
        startTime = Time.time;
        while (Time.time - startTime < fadeDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeDuration;
            canvasGroup.alpha = 1 - normalizedTime;
            yield return null;
        }

        // Destroy this gameObject
        SummonManager.Instance.RemoveMonsterFromQueue(this);
        Destroy(this.gameObject);
    }
}
