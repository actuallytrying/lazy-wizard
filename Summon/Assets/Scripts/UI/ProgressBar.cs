using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float fillTime = 5f;
    private Image progressBarFill;

    private Action onFillComplete; // Delegate to be called when the bar is filled.

    private void OnEnable()
    {
        progressBarFill = GetComponentInChildren<Image>();
    }

    public void StartFilling(float time)
    {
        StopAllCoroutines();
        fillTime = time;
        StartCoroutine(FillProgressBar());
    }

    public void StartFilling(Action fillCompleteCallback, float time)
    {
        StopAllCoroutines();
        onFillComplete = fillCompleteCallback; // Assign the callback
        fillTime = time;
        StartCoroutine(FillProgressBar());
    }

    private IEnumerator FillProgressBar()
    {
        float startTime = Time.time;

        while (Time.time - startTime <= fillTime)
        {
            float normalizedTime = (Time.time - startTime) / fillTime;
            if (progressBarFill)
            {
                progressBarFill.fillAmount = normalizedTime;
            }
            
            yield return null;
        }

        progressBarFill.fillAmount = 1;

        onFillComplete?.Invoke(); // If onFillComplete is not null, call the method it points to.
    }
}

