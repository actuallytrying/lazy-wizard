using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FighterUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image iconBorder;

    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconContainer; // Changed to Transform for easier manipulation

    public void SetFighter(IFighter fighter)
    {
        icon.sprite = fighter.Image;
        powerText.text = fighter.Power.ToString();
        nameText.text = fighter.Title;
        powerText.color = Color.white; // Set color back to default when new fighter is set

        // Clear container
        foreach (Transform child in iconContainer)
        {
            Destroy(child.gameObject);
        }

        // Add class icon
        GameObject classIcon = Instantiate(iconPrefab, iconContainer);
        classIcon.GetComponent<IconHandler>().SetIcon(SpriteManager.Instance.GetSpriteByClass(fighter.Class));

        // Add element icon
        if (fighter.Element == Element.None) return;
        GameObject elementIcon = Instantiate(iconPrefab, iconContainer);
        elementIcon.GetComponent<IconHandler>().SetIcon(SpriteManager.Instance.GetSpriteByElement(fighter.Element));
    }

    public void SetPower(int power, bool? boosted)
    {
        powerText.text = power.ToString();

        if (boosted != null)
        {
            if ((bool)boosted)
            {
                powerText.color = Color.yellow; // golden color when boosted
            }
            else
            {
                powerText.color = Color.red; // red color when decreased
            }
        }
    }
}


