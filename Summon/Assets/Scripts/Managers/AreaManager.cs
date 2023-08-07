using UnityEngine;
using System.Collections.Generic;

public class AreaManager : MonoBehaviour
{
    // Singleton instance
    public static AreaManager Instance { get; private set; }

    public List<Area> areas;
    public int currentAreaIndex = 0;

    public Area CurrentArea => areas[currentAreaIndex];

    public delegate void AreaChangeDelegate(Area newArea);
    public event AreaChangeDelegate OnAreaChanged;

    private void Awake()
    {
        // Ensure there's only one instance of AreaManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optionally, prevent destruction between scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MoveToNextArea()
    {
        if (currentAreaIndex < areas.Count - 1)  // only increment if not already at the last area
        {
            currentAreaIndex++;
            NotifyAreaChanged();
        }
    }

    public void MoveToPreviousArea()
    {
        if (currentAreaIndex > 0)  // only decrement if not already at the first area
        {
            currentAreaIndex--;
            NotifyAreaChanged();
        }
    }

    private void NotifyAreaChanged()
    {
        OnAreaChanged?.Invoke(CurrentArea);
    }
}


