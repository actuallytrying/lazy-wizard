using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroAlphaOnLaunch : MonoBehaviour
{
    void Awake() {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
