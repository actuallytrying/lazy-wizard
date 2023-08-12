using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaStats", menuName = "Stats/AreaStats")]
public class AreaStats : ScriptableObject
{
    public string title;
    public int wins;
    public int losses;
    public int totalExperience;
    public int totalLevelsGained;

    // New fields for session and last encounter stats
    public int startWins;
    public int startLosses;
    public int startTotalExperience;
    public int startTotalLevelsGained;

    public int lastEncounterExperience;
    public int lastEncounterLevelsGained;

    // Add event
    public event Action OnStatsUpdated = delegate { };
    public event Action<bool, int, int> OnLastEncounterUpdated = delegate { };
    public event Action OnNewEncounterStarted = delegate { };


    // Location to save/load the data
    private string dataPath => Path.Combine(Application.persistentDataPath, $"{title}.json");

    public void InitializeSessionStats()
    {
        // Initialize session start stats with the current stats
        startWins = wins;
        startLosses = losses;
        startTotalExperience = totalExperience;
        startTotalLevelsGained = totalLevelsGained;
    }

    public void OnLoss()
    {
        losses++;
        lastEncounterExperience = 0;
        lastEncounterLevelsGained = 0;
        OnLastEncounterUpdated(false, lastEncounterExperience, lastEncounterLevelsGained);
        SaveData();
    }

    public void OnWin(int experienceGained, int levelsGained)
    {
        wins++;
        totalExperience += experienceGained;
        totalLevelsGained += levelsGained;
        lastEncounterExperience = experienceGained;
        lastEncounterLevelsGained = levelsGained;
        OnLastEncounterUpdated(true, lastEncounterExperience, lastEncounterLevelsGained);
        SaveData();
    }

    public void OnNewEncounter()
    {
        OnNewEncounterStarted();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(dataPath, json);
        OnStatsUpdated();
    }

    public void LoadData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            JsonUtility.FromJsonOverwrite(json, this);

            // Initialize session start stats with the current stats
            startWins = wins;
            startLosses = losses;
            startTotalExperience = totalExperience;
            startTotalLevelsGained = totalLevelsGained;
        }
    }


    // New methods to calculate session stats
    public int SessionWins => wins - startWins;
    public int SessionLosses => losses - startLosses;
    public int SessionTotalExperience => totalExperience - startTotalExperience;
    public int SessionTotalLevelsGained => totalLevelsGained - startTotalLevelsGained;
}



