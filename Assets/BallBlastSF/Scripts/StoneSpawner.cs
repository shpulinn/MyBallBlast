using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Stone stonePrefab;
    [SerializeField] private Transform[] stoneSpawnPoints;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private Color[] colors;
    
    [Header("Spawn Balance")]
    [SerializeField] private int amount = 1;
    [SerializeField] private Turret turret;
    [SerializeField] [Range(0f, 1f)] private float minHealthPercentage;
    [SerializeField] private float maxHealthRate;
    
    [Header("Events")]
    public UnityEvent OnSpawnComplete;
    
    private List<Stone> spawnedStones = new List<Stone>();
    private int stoneMaxHealth;
    private int stoneMinHealth;
    private float timer = 0f;
    private int spawnedAmount = 0;

    private void Start()
    {
        int damagePerSecond = (int)(turret.Damage * turret.ProjectileAmount * (1 / turret.FireRate));
        
        stoneMaxHealth = (int)(damagePerSecond * maxHealthRate);
        stoneMinHealth = (int)(stoneMaxHealth * minHealthPercentage);
        
        timer = spawnRate;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            Spawn();
            
            timer = 0f;
        }

        if (spawnedAmount >= amount)
        {
            OnSpawnComplete?.Invoke();
            enabled = false;
        }
    }

    private void Spawn()
    {
        Stone stone = Instantiate(
            stonePrefab,
            stoneSpawnPoints[Random.Range(0, stoneSpawnPoints.Length)].position,
            Quaternion.identity);
        stone.Init(this, (Stone.StoneSize)Random.Range(1, 4));
        stone.maxHealth = Random.Range(stoneMinHealth, stoneMaxHealth + 1);
        var randomColor = GetRandomColor();
        stone.GetComponent<StoneView>().SetColor(randomColor);
        spawnedStones.Add(stone);
        
        spawnedAmount++;
    }

    public Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }

    public void AddStone(Stone stone)
    {
        if (!spawnedStones.Contains(stone))
        {
            spawnedStones.Add(stone);
        }
    }

    public void RemoveStone(Stone stone)
    {
        if (!spawnedStones.Contains(stone)) return;
        
        spawnedStones.Remove(stone);
            
        if (spawnedAmount >= amount && spawnedStones.Count == 0)
        {
            OnSpawnComplete?.Invoke();
        }
    }

    public int GetStonesAmount()
    {
        return spawnedStones.Count;
    }
    
    public void ApplyToAllStones(System.Action<Stone> action)
    {
        foreach (var stone in spawnedStones.ToList())
        {
            if (stone != null)
                action(stone);
        }
    }

    public void ApplyToAllMovements(System.Action<StoneMovement> action)
    {
        ApplyToAllStones(stone => action(stone.GetComponent<StoneMovement>()));
    }
}