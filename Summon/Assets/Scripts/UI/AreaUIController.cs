using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AreaUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Image background;
    void Start()
    {
        AreaManager.Instance.OnAreaChanged += HandleAreaChange;  // subscribe to the OnAreaChanged event
        HandleAreaChange(AreaManager.Instance.CurrentArea);  // handle the initial area
    }

    private void OnDestroy()
    {
        AreaManager.Instance.OnAreaChanged -= HandleAreaChange;  // don't forget to unsubscribe!
    }

    private void HandleAreaChange(Area newArea)
    {
        title.text = (AreaManager.Instance.currentAreaIndex + 1) + ": " + newArea.areaName;
        background.sprite = newArea.backgroundImage;
    }
}
