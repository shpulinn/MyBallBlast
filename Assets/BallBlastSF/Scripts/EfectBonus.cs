public abstract class EffectBonus : DroppableBase
{
    protected abstract IAbility CreateAbility();

    public override void OnCollected(Cart cart)
    {
        var ability = CreateAbility();
        ability?.Activate();
        PlayCollectionSound();
    }

    protected abstract void PlayCollectionSound();
}