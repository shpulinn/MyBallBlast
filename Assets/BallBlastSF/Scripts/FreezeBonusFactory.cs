public class FreezeBonusFactory : DropFactoryBase<FreezeBonus>
{
    private readonly StoneSpawner spawner;
    private readonly FreezeScreenEffect visualEffect;

    public FreezeBonusFactory(FreezeBonus prefab, StoneSpawner spawner, FreezeScreenEffect visualEffect) 
        : base(prefab)
    {
        this.spawner = spawner;
        this.visualEffect = visualEffect;
    }

    protected override void ConfigureInstance(FreezeBonus instance)
    {
        instance.Initialize(spawner, visualEffect);
    }
}