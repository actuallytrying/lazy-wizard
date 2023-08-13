using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoColoredContainer : MonoBehaviour
{
    [SerializeField] Image border;
    [SerializeField] Image background;

    void Awake() {
        border.color = UIColors.Primary;
        background.color = UIColors.Secondary;
    }
}
