using UnityEngine;

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{
    public enum StoneSize
    {
        Small, Medium, Big, Huge
    }
    
    [SerializeField] private StoneSize size;
    [SerializeField] private float spawnUpForce;
    [SerializeField] private StoneView view;
    
    [Header("Coins")]
    [SerializeField] [Range(0.1f, 1f)] private float coinSpawnChance;
    [SerializeField] [Range(0.1f, 1f)] private float bonusSpawnChance;
    
    private StoneMovement movement;
    private StoneSpawner spawner;

    private void Awake()
    {
        movement = GetComponent<StoneMovement>();
        
        OnDeath.AddListener(OnStoneDestroyed);
    }

    public void Init(StoneSpawner spawner, StoneSize size)
    {
        this.spawner = spawner;
        
        SetSize(size);
    }

    private void SetSize(StoneSize size)
    {
        if (size < 0) return;
        
        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    private Vector3 GetVectorFromSize(StoneSize size)
    {
        switch (size)
        {
            case StoneSize.Small:
                return new Vector3(0.4f, 0.4f, 0.4f);
            case StoneSize.Medium:
                return new Vector3(0.6f, 0.6f, 0.6f);
            case StoneSize.Big:
                return new Vector3(0.8f, 0.8f, 0.8f);
            case StoneSize.Huge:
                return new Vector3(1f, 1f, 1f);
            default:
                return Vector3.one;
        }
    }
    
    private void OnStoneDestroyed()
    {
        if (size != StoneSize.Small)
        {
            SpawnStones();
        }
        SpawnCoin();
        spawner.RemoveStone(this);
        view.PlayDestroyParticle();
        
        Destroy(gameObject);
    }

    private void SpawnStones()
    {
        for (int i = 0; i < 2; i++)
        {
            Stone stone = Instantiate(this, transform.position, Quaternion.identity);
            stone.Init(spawner, size - 1);
            stone.maxHealth = Mathf.Clamp(maxHealth / 2, 1, maxHealth);
            stone.movement.AddVerticalVelocity(spawnUpForce);
            stone.movement.SetHorizontalDirection((i % 2 * 2) - 1);
            stone.transform.position += new Vector3(0, 0, transform.position.z + 0.01f * (i + 1));
            stone.GetComponent<StoneView>().SetColor(spawner.GetRandomColor());
            spawner.AddStone(stone);
        }
    }

    private void SpawnCoin()
    {
        var chance = Random.Range(0f, 1f);
        if (chance < bonusSpawnChance)
        {
            //DropsFactoryRegistry.Instance.Create<FreezeBonus>(transform.position);
            DropsFactoryRegistry.Instance.Create<ShieldBonus>(transform.position);
        }
        else if (chance < coinSpawnChance)
        {
            DropsFactoryRegistry.Instance.Create<Coin>(transform.position);
        }
    }

    private void OnDestroy()
    {
        OnDeath.RemoveListener(OnStoneDestroyed);
    }
}
