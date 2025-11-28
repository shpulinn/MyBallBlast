using UnityEngine;

public class Coin : DroppableBase
{
    [SerializeField] private int value = 1;
    
    public override void OnCollected(Cart cart)
    {
        cart.AddCoins(value);
        SoundManager.PlaySound(SoundType.Coin);
        Destroy(gameObject);
    }
}
