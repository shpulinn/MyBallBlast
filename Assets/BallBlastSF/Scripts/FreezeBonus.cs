using UnityEngine;

public class FreezeBonus : EffectBonus
{
    [SerializeField] private float duration = 6f;

    private StoneSpawner spawner;
    private FreezeScreenEffect screenVisualEffect;

    public void Initialize(StoneSpawner spawner, FreezeScreenEffect screenVisualEffect)
    {
        this.spawner = spawner;
        this.screenVisualEffect = screenVisualEffect;
    }
    protected override IAbility CreateAbility()
    {
        return new FreezeAbility(spawner, screenVisualEffect, duration);
    }

    protected override void PlayCollectionSound()
    {
        SoundManager.PlaySound(SoundType.Freeze);
    }
}