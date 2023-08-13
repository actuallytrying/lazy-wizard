using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFighter
{
    string Title { get; }
    Sprite Image { get; }
    Class Class { get; }
    Element Element { get; }
    Rarity Rarity { get; }
    int Power { get; }
}

