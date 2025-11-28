using System.Collections;
using UnityEngine;

public class ShieldAbility : IAbility
{
    private readonly Cart cart;
    private readonly ShieldVisualEffect shieldVisualEffect;
    private readonly float duration;
    
    public ShieldAbility(Cart cart, ShieldVisualEffect shieldVisualEffect, float duration = 5f)
    {
        this.cart = cart;
        this.shieldVisualEffect = shieldVisualEffect;
        this.duration = duration;
    }
    
    public void Activate()
    {
        cart.ActivateShield();
        
        shieldVisualEffect.Activate();

        CoroutineRunner.Start(UnfreezeAfter(duration));
    }

    private IEnumerator UnfreezeAfter(float delay)
    {
        yield return new WaitForSeconds(delay);

        cart.DeactivateShield();
        
        shieldVisualEffect.Deactivate();
    }
}