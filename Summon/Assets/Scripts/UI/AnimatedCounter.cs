using System.Collections;
using TMPro;
using UnityEngine;

public class AnimatedCounter : MonoBehaviour
{
    public float animationDuration = 1.0f; // How long the animation will take
    public float delayBeforeAnimation = 0.25f; // How long to wait before starting the animation
    [SerializeField] private TextMeshProUGUI counterText;

    public void SetNumber(int original, int newValue)
    {
        int difference = newValue - original;

        if (difference != 0)
        {
            counterText.text = $"{original} + {difference}";
            StartCoroutine(AnimateCounterChange(original, difference));
        }
        else
        {
            counterText.text = newValue.ToString();
        }
    }

    private IEnumerator AnimateCounterChange(int original, int change)
    {
        // Delay before animation starts
        yield return new WaitForSeconds(delayBeforeAnimation);

        float timer = 0;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            int increment = Mathf.RoundToInt(Mathf.Lerp(0, change, timer / animationDuration));
            counterText.text = $"{original + increment} + {change - increment}";
            yield return null;
        }

        // At the end of animation, ensure we reach the exact target number
        counterText.text = (original + change).ToString();
    }

}
