using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdventureManager : MonoBehaviour
{
    [SerializeField] private FighterUI monsterUI;
    [SerializeField] private FighterUI enemyUI;
    [SerializeField] private ProgressBar combatProgressBar;
    [SerializeField] private TextMeshProUGUI monstersLeftText;

    private AreaStats areaStats;  // now private, will be set by AreaManager

    public List<Monster> monsterList;
    public List<EnemyTemplate> enemyList;
    private List<Monster> foughtMonsters = new List<Monster>();
    private Dictionary<Monster, int> monsterDamage = new Dictionary<Monster, int>();

    public float combatInterval = 5f;
    public float combatDuration = 2.5f;
    public float waitTimeAfterRound = 2f;

    void Start()
    {
        AreaManager.Instance.OnAreaChanged += HandleAreaChange;  // subscribe to the OnAreaChanged event
        HandleAreaChange(AreaManager.Instance.CurrentArea);  // handle the initial area
    }

    private void OnDestroy()
    {
        AreaManager.Instance.OnAreaChanged -= HandleAreaChange;  // don't forget to unsubscribe!
    }

    private void HandleAreaChange(Area newArea)
    {
        // Load new enemies, area stats etc.
        enemyList = newArea.enemies;
        areaStats = newArea.areaStats;
        // Restart combat
        StopAllCoroutines();  // if combat is a coroutine
        InitializeAdventure();
    }

    private void InitializeAdventure()
    {
        StartCoroutine(CombatLoop());
    }

    private IEnumerator CombatLoop()
    {
        while (true)
        {
            areaStats.OnNewEncounter();
            int preCombatLevels = GetTotalMonsterLevels();
            monsterList = MonsterManager.Instance.GetActiveMonsters();
            Enemy enemy = SpawnEnemy();
            foughtMonsters.Clear();
            monsterDamage.Clear();

            while (AreMonstersRemaining())
            {
                Monster monster = DrawMonster();
                int monsterPower = CalculateMonsterPower(monster, enemy);
                monsterUI.SetFighter(monster);
                bool? boosted = monster.Power != monsterPower ? monster.Power < monsterPower : null;
                monsterUI.SetPower(monsterPower, boosted);
                UpdateMonstersLeftText();

                yield return ExecuteCombat(monster, monsterPower, enemy);

                if (enemy.Power <= 0)
                {

                    Victory(monster, enemy);
                    int postCombatLevels = GetTotalMonsterLevels();
                    areaStats.OnWin(enemy.experienceDrop, postCombatLevels - preCombatLevels);
                    yield return new WaitForSeconds(waitTimeAfterRound);
                    
                    break;
                }
            }

            if (!AreMonstersRemaining() && enemy.Power > 0)
            {
                areaStats.OnLoss();
                yield return new WaitForSeconds(waitTimeAfterRound);
            }

        }
    }

    private Enemy SpawnEnemy()
    {
        EnemyTemplate enemyTemplate = enemyList[Random.Range(0, enemyList.Count)];
        Enemy enemy = new Enemy(enemyTemplate);
        enemyUI.SetFighter(enemy);
        return enemy;
    }

    private Monster DrawMonster()
    {
        Monster monster;
        do
        {
            monster = monsterList[Random.Range(0, monsterList.Count)];
        }
        while (foughtMonsters.Contains(monster));

        foughtMonsters.Add(monster);
        return monster;
    }

    private int CalculateMonsterPower(Monster monster, Enemy enemy)
    {
        return Mathf.RoundToInt(monster.Power * CombatHelper.GetClassMultiplier(monster.Class, enemy.Class) * CombatHelper.GetElementMultiplier(monster.Element, enemy.Element));
    }

    private void UpdateMonstersLeftText()
    {
        monstersLeftText.text = "Summons left: " + (monsterList.Count - foughtMonsters.Count);
    }

    private IEnumerator ExecuteCombat(Monster monster, int monsterPower, Enemy enemy)
    {
        combatProgressBar.StartFilling(combatDuration);
        yield return new WaitForSeconds(combatDuration);

        int damageDone = Mathf.Min(enemy.Power, monsterPower);
        enemy.power = Mathf.Max(0, enemy.Power - damageDone);
        enemyUI.SetFighter(enemy);

        if (monsterDamage.ContainsKey(monster))
        {
            monsterDamage[monster] += damageDone;
        }
        else
        {
            monsterDamage.Add(monster, damageDone);
        }
    }

    private bool AreMonstersRemaining()
    {
        return foughtMonsters.Count < monsterList.Count;
    }

    private void Victory(Monster lastMonster, Enemy enemy)
    {
        Debug.Log("Monster " + lastMonster.Title + " has defeated the enemy " + enemy.Title + "!");
        DistributeExperience(lastMonster, enemy);
        ItemManager.Instance.OnEnemyDefeat(enemy);
    }

    private void DistributeExperience(Monster lastMonster, Enemy enemy)
    {
        int totalDamageDone = 0;
        foreach (var damage in monsterDamage.Values)
        {
            totalDamageDone += damage;
        }

        foreach (var entry in monsterDamage)
        {
            Monster monster = entry.Key;
            int damageDone = entry.Value;

            float damagePercentage = (float)damageDone / totalDamageDone;
            int experienceAwarded = Mathf.RoundToInt(damagePercentage * enemy.experienceDrop);
            monster.AddExperience(experienceAwarded);

            Debug.Log($"Monster {monster.Title} did {damageDone} damage ({damagePercentage * 100:0.##}%) to enemy {enemy.Title} and received {experienceAwarded} experience.");
        }
    }

    private int GetTotalMonsterLevels()
    {
        int totalLevels = 0;
        foreach (var monster in monsterList)
        {
            totalLevels += monster.level;
        }
        return totalLevels;
    }


}

