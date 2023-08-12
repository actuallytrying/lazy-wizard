using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyTemplate")]
public class EnemyTemplate : ScriptableObject
{
    public string title;
    public Sprite image;
    public Class enemyClass;
    public int basePower;

    public List<Item> drops;
    public int experienceDrop;
}

