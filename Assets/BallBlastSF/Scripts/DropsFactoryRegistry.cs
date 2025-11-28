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
        RegisterFactory(new CoinFactory(coinPrefab));
        RegisterFactory(new FreezeBonusFactory(freezeBonusPrefab, spawner, freezeScreenEffect));
        RegisterFactory(new ShieldBonusFactory(shieldBonusPrefab, cart, shieldVisualEffect));
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
}