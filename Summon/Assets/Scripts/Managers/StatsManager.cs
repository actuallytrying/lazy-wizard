using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    public float fireBonus = 1.0f;
    public float waterBonus = 1.0f;
    public float earthBonus = 1.0f;
    public float windBonus = 1.0f;

    public float experienceBonus = 1.0f;
    public float powerBonus = 1.0f;
    public float dropChanceBonus = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
