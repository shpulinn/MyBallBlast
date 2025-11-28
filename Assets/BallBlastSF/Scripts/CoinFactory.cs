public class CoinFactory : DropFactoryBase<Coin>
{
    public CoinFactory(Coin prefab) : base(prefab) { }

    protected override void ConfigureInstance(Coin instance)
    {
        
    }
}