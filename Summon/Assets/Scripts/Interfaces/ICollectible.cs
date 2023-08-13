using UnityEngine;

public interface ICollectible
{
    string GetTitle();
    Sprite GetImage();
    bool IsUnlocked();
}
