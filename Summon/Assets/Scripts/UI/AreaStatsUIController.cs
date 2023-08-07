using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AreaStatsUIController : MonoBehaviour
{
    // The AreaStats object that this UI is displaying.
    public AreaStats areaStats;

    // Historic UI
    public TextMeshProUGUI historicTitle;
    public TextMeshProUGUI historicWinsLosses;
    public TextMeshProUGUI historicTotalExperience;
    public TextMeshProUGUI historicTotalLevelsGained;

    // Session UI
    public TextMeshProUGUI sessionTitle;
    public TextMeshProUGUI sessionWinsLosses;
    public TextMeshProUGUI sessionTotalExperience;
    public TextMeshProUGUI sessionTotalLevelsGained;

    // Last Encounter UI
    public CanvasGroup resultContainer;
    public TextMeshProUGUI lastEncounterTitle;
    public TextMeshProUGUI lastEncounterResult;
    public TextMeshProUGUI lastEncounterExperienceGained;
    public TextMeshProUGUI lastEncounterLevelsGained;


    private void HandleAreaChange(Area newArea)
    {
        // Unsubscribe from old area's events
        if (areaStats != null)
        {
            areaStats.OnStatsUpdated -= UpdateUI;
            areaStats.OnLastEncounterUpdated -= UpdateLastEncounterStats;
            areaStats.OnNewEncounterStarted -= ClearLastEncounterStats;
        }

        // Update the current area
        areaStats = newArea.areaStats;

        // Initialize the session stats for the new area
        areaStats.InitializeSessionStats();

        // Subscribe to new area's events
        areaStats.OnStatsUpdated += UpdateUI;
        areaStats.OnLastEncounterUpdated += UpdateLastEncounterStats;
        areaStats.OnNewEncounterStarted += ClearLastEncounterStats;
    }



    void Start()
    {
        // Subscribe to events
        areaStats.OnStatsUpdated += UpdateUI;
        areaStats.OnLastEncounterUpdated += UpdateLastEncounterStats;
        areaStats.OnNewEncounterStarted += ClearLastEncounterStats;
        AreaManager.Instance.OnAreaChanged += HandleAreaChange;  
        HandleAreaChange(AreaManager.Instance.CurrentArea);
        // Initialize the UI
        UpdateUI();
    }

    void OnDestroy()
    {
        // Unsubscribe from events
        areaStats.OnStatsUpdated -= UpdateUI;
        areaStats.OnLastEncounterUpdated -= UpdateLastEncounterStats;
        areaStats.OnNewEncounterStarted -= ClearLastEncounterStats;
        AreaManager.Instance.OnAreaChanged -= HandleAreaChange; 
    }

    public void UpdateUI()
    {
        // Calculate win loss ratio for historic stats
        float historicWLRatio = (areaStats.wins == 0 && areaStats.losses == 0) ? 0 : (float)areaStats.wins / (areaStats.losses + areaStats.wins) * 100;

        // Calculate win loss ratio for session stats
        float sessionWLRatio = (areaStats.SessionWins == 0 && areaStats.SessionLosses == 0) ? 0 : (float)areaStats.SessionWins / (areaStats.SessionLosses + areaStats.SessionWins) * 100;

        // Update historic stats
        historicTitle.text = "Historic";
        historicWinsLosses.text = areaStats.wins + " / " + areaStats.losses + " ( " + historicWLRatio.ToString("F1") + "% )";
        historicTotalExperience.text = areaStats.totalExperience.ToString();
        historicTotalLevelsGained.text = areaStats.totalLevelsGained.ToString();

        // Update session stats
        sessionTitle.text = "Session";
        sessionWinsLosses.text = areaStats.SessionWins + " / " + areaStats.SessionLosses + " ( " + sessionWLRatio.ToString("F1") + "% )";
        sessionTotalExperience.text = areaStats.SessionTotalExperience.ToString();
        sessionTotalLevelsGained.text = areaStats.SessionTotalLevelsGained.ToString();

        // Update last encounter stats
        lastEncounterTitle.text = "Result";
    }



    public void UpdateLastEncounterStats(bool victory, int experienceGained, int levelsGained)
    {
        resultContainer.alpha = 1;
        // Update the result
        lastEncounterResult.text = victory ? "Victory" : "Defeat";
        lastEncounterResult.color = victory ? Color.green : Color.red;

        if (victory)
        {
            // Update the experience gained
            lastEncounterExperienceGained.text = "Experience:\n" + experienceGained;

            // Update the levels gained
            lastEncounterLevelsGained.text = "Levels:\n" + levelsGained;
        }
    }

    public void ClearLastEncounterStats()
    {
        resultContainer.alpha = 0;
        lastEncounterResult.text = "";
        lastEncounterExperienceGained.text = "";
        lastEncounterLevelsGained.text = "";
    }
}
