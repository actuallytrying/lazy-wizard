using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonManager : MonoBehaviour
{
    public Tier currentTier = Tier.First;
    public MonsterBook book;

    public int summonCharges = 0;
    [SerializeField] private int maxCharges = 10;
    [SerializeField] private float chargeInterval = 0.25f * 60f; // 6 minutes
    private float nextChargeTime;
    public GameObject chargeSlotContainer; 
    public GameObject filledSlotPrefab; 
    public GameObject emptySlotPrefab;

    [SerializeField] private GameObject monsterResultPrefab;
    [SerializeField] private Transform summonResultContainer;

    private Queue<MonsterResult> summonedMonstersQueue = new Queue<MonsterResult>();

    public static SummonManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Multiple instances of SummonManager detected. Destroying one of them.");
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        // Start charging routine
        UpdateChargeUI();
        StartCoroutine(ChargeRegeneration());
    }

    private IEnumerator ChargeRegeneration()
    {
        while (true)
        {
            if (summonCharges < maxCharges)
            {
                if (Time.time >= nextChargeTime)
                {
                    summonCharges += 1;
                    UpdateChargeUI();
                    nextChargeTime = Time.time + chargeInterval;
                }
            }
            yield return new WaitForSeconds(chargeInterval);
        }
    }

    public void UpdateChargeUI()
    {
        // Destroy old slots
        foreach (Transform child in chargeSlotContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Create new slots
        for (int i = 0; i < maxCharges; i++)
        {
            if (i < summonCharges)
            {
                // Instantiate a filled slot
                Instantiate(filledSlotPrefab, chargeSlotContainer.transform);
            }
            else
            {
                // Instantiate an empty slot
                Instantiate(emptySlotPrefab, chargeSlotContainer.transform);
            }
        }
    }

    public void SummonMonsters()
    {
        if (summonCharges > 0)
        {
            // Draw a monster
            Monster drawnMonster = DrawMonster();

            // Gain 1-4 levels
            int levelsGained = Random.Range(1, 5);
            drawnMonster.AddLevels(levelsGained);

            // Instantiate MonsterResult prefab and configure it
            GameObject monsterResultObject = Instantiate(monsterResultPrefab, summonResultContainer);
            MonsterResult monsterResult = monsterResultObject.GetComponent<MonsterResult>();
            if (monsterResult != null)
            {
                monsterResult.SetMonster(drawnMonster, levelsGained);

                // Add the monster to the queue
                summonedMonstersQueue.Enqueue(monsterResult);

                // Start the FadeAndDestroyRoutine for the first monster in the queue
                if (summonedMonstersQueue.Count == 1)
                {
                    StartCoroutine(monsterResult.FadeAndDestroyRoutine());
                }
            }

            summonCharges -= 1;
            UpdateChargeUI();
        }
    }


    private Monster DrawMonster()
    {
        // Get pages for the current tier
        List<Page> currentTierPages = book.GetPagesByTier(currentTier);

        // Draw rarity
        float rarityDraw = Random.value;
        List<Monster> rarityPool = new List<Monster>();

        if (rarityDraw < 0.01)
            AddMonstersByRarityToPool(Rarity.Legendary, currentTierPages, rarityPool);
        else if (rarityDraw < 0.05)
            AddMonstersByRarityToPool(Rarity.Epic, currentTierPages, rarityPool);
        else if (rarityDraw < 0.20)
            AddMonstersByRarityToPool(Rarity.Rare, currentTierPages, rarityPool);
        else
            AddMonstersByRarityToPool(Rarity.Common, currentTierPages, rarityPool);

        // Draw specific monster within rarity category
        Monster monster = DrawSpecificMonster(rarityPool);
        return monster;
    }

    private void AddMonstersByRarityToPool(Rarity rarity, List<Page> pages, List<Monster> pool)
    {
        foreach (Page page in pages)
        {
            pool.AddRange(page.GetMonstersByRarity(rarity));
        }
    }

    private Monster DrawSpecificMonster(List<Monster> rarityPool)
    {
        // Create a list of cumulative weights
        List<float> cumulativeWeights = new List<float>();
        float totalWeight = 0;

        for (int i = 0; i < rarityPool.Count; i++)
        {
            // This line makes the first items more likely than the last ones
            float weight = rarityPool.Count - i;
            totalWeight += weight;
            cumulativeWeights.Add(totalWeight);
        }

        // Select a random weight within the total sum
        float randomWeight = Random.Range(0, totalWeight);

        // Find the index where our random weight falls into
        int randomIndex = cumulativeWeights.FindIndex(weight => weight >= randomWeight);

        // Return the monster at the random index
        return rarityPool[randomIndex];
    }

    public void RemoveMonsterFromQueue(MonsterResult monsterResult)
    {
        if (summonedMonstersQueue.Count > 0 && summonedMonstersQueue.Peek() == monsterResult)
        {
            summonedMonstersQueue.Dequeue();

            // Start the FadeAndDestroyRoutine for the next monster in the queue
            if (summonedMonstersQueue.Count > 0)
            {
                StartCoroutine(summonedMonstersQueue.Peek().FadeAndDestroyRoutine());
            }
        }
    }
}


