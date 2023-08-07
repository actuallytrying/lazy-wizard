using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Area", menuName = "Area")]
public class Area : ScriptableObject
{
    public string areaName;
    public List<EnemyTemplate> enemies;
    public AreaStats areaStats;
    public Sprite backgroundImage;
    public List<ItemEffect> itemCompletionEffects;
}

