using System.Collections;
using UnityEngine;

public class FreezeAbility : IAbility
{
    private readonly StoneSpawner spawner;
    private readonly FreezeScreenEffect screenVisualEffect;
    private readonly float duration;

    public FreezeAbility(StoneSpawner spawner, FreezeScreenEffect screenVisualEffect, float duration = 5f)
    {
        this.spawner = spawner;
        this.screenVisualEffect = screenVisualEffect;
        this.duration = duration;
    }

    public void Activate()
    {
        spawner.ApplyToAllMovements(movement =>
        {
            if (movement != null)
                movement.Freeze();
        });
        
        screenVisualEffect.FreezeScreen();

        CoroutineRunner.Start(UnfreezeAfter(duration));
    }

    private IEnumerator UnfreezeAfter(float delay)
    {
        yield return new WaitForSeconds(delay);

        spawner.ApplyToAllMovements(movement =>
        {
            if (movement != null)
                movement.Unfreeze();
        });
        
        screenVisualEffect.UnfreezeScreen();
    }
}