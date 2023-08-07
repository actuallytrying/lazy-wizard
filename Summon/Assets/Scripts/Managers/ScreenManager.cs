using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] CanvasGroup activeScreen;

    void Awake()
    {
        InitializeScreen(activeScreen);
    }

    private void InitializeScreen(CanvasGroup screen)
    {
        SetCanvasGroupActive(screen, true);
    }

    public void ChangeToScreen(CanvasGroup screen)
    {
        SetCanvasGroupActive(activeScreen, false);
        activeScreen = screen;
        SetCanvasGroupActive(activeScreen, true);

        IScreen screenComponent = activeScreen.GetComponent<IScreen>();
        if (screenComponent != null)
        {
            screenComponent.OnActivate();
        }
    }

    private void SetCanvasGroupActive(CanvasGroup canvasGroup, bool active)
    {
        if (active)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}

