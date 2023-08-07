using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance { get; private set; }

    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite earthSprite;
    [SerializeField] private Sprite windSprite;

    [SerializeField] private Sprite meleeSprite;
    [SerializeField] private Sprite rangedSprite;
    [SerializeField] private Sprite magicSprite;

    private Dictionary<Element, Sprite> elementSprites;
    private Dictionary<Class, Sprite> classSprites;

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

        elementSprites = new Dictionary<Element, Sprite>
        {
            { Element.None, null },
            { Element.Fire, fireSprite },
            { Element.Water, waterSprite },
            { Element.Earth, earthSprite },
            { Element.Wind, windSprite },
        };

        classSprites = new Dictionary<Class, Sprite>
        {
            { Class.Melee, meleeSprite },
            { Class.Ranged, rangedSprite },
            { Class.Magic, magicSprite },
        };
    }

    public Sprite GetSpriteByElement(Element element)
    {
        elementSprites.TryGetValue(element, out Sprite sprite);
        return sprite;
    }

    public Sprite GetSpriteByClass(Class classType)
    {
        classSprites.TryGetValue(classType, out Sprite sprite);
        return sprite;
    }
}

