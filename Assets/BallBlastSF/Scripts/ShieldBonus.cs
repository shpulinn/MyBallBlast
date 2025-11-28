using UnityEngine;

public class ShieldBonus : EffectBonus
{
    [SerializeField] private float duration = 6f;

    private Cart cart;
    private ShieldVisualEffect shieldVisualEffect;
    
    public void Initialize(Cart cart, ShieldVisualEffect shieldVisualEffect)
    {
        this.cart = cart;
        this.shieldVisualEffect = shieldVisualEffect;
    }
    protected override IAbility CreateAbility()
    {
        return new ShieldAbility(cart, shieldVisualEffect, duration);
    }

    protected override void PlayCollectionSound()
    {
        SoundManager.PlaySound(SoundType.ShieldOn);
    }
}