using System.Collections.Generic;
using UnityEngine;

public class DropsFactoryRegistry : MonoBehaviour
{
    public static DropsFactoryRegistry Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private FreezeBonus freezeBonusPrefab;
    [SerializeField] private ShieldBonus shieldBonusPrefab;

    [Header("Dependencies")]
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] private FreezeScreenEffect freezeScreenEffect;
    [SerializeField] private ShieldVisualEffect shieldVisualEffect;
    [SerializeField] private Cart cart;

    private Dictionary<System.Type, object> factories = new();
    private List<IDropFactory> allBonusesFactories = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeFactories();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFactories()
    {
        var coinFactory = new CoinFactory(coinPrefab);
        var freezeFactory = new FreezeBonusFactory(freezeBonusPrefab, spawner, freezeScreenEffect);
        var shieldFactory = new ShieldBonusFactory(shieldBonusPrefab, cart, shieldVisualEffect);

        RegisterFactory(coinFactory);
        RegisterFactory(freezeFactory);
        RegisterFactory(shieldFactory);
        
        allBonusesFactories.Add(freezeFactory);
        allBonusesFactories.Add(shieldFactory);
    }

    public void RegisterFactory<T>(IDropFactory<T> factory) where T : IDroppable
    {
        factories[typeof(T)] = factory;
    }

    public T Create<T>(Vector3 position) where T : IDroppable
    {
        if (factories.TryGetValue(typeof(T), out var factory))
        {
            return ((IDropFactory<T>)factory).Create(position);
        }

        Debug.LogError($"Factory for type {typeof(T)} not found!");
        return default;
    }
    
    public IDroppable CreateRandom(Vector3 position)
    {
        if (allBonusesFactories.Count == 0)
        {
            Debug.LogError("No drop factories available!");
            return null;
        }

        int index = Random.Range(0, allBonusesFactories.Count);
        return allBonusesFactories[index].Create(position);
    }
}