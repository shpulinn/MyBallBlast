public class ShieldBonusFactory : DropFactoryBase<ShieldBonus>
{
    private readonly Cart cart;
    private readonly ShieldVisualEffect shieldVisualEffect;

    public ShieldBonusFactory(ShieldBonus prefab, Cart cart, ShieldVisualEffect shieldVisualEffect) 
        : base(prefab)
    {
        this.cart = cart;
        this.shieldVisualEffect = shieldVisualEffect;
    }

    protected override void ConfigureInstance(ShieldBonus instance)
    {
        instance.Initialize(cart, shieldVisualEffect);
    }
}