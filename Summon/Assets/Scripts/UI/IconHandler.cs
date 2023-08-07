using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}

