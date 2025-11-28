using UnityEngine;

public abstract class DroppableBase : MonoBehaviour, IDroppable
{
    public abstract void OnCollected(Cart cart);

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.root.TryGetComponent<Cart>(out Cart cart)) return;
        
        OnCollected(cart);
        Destroy(gameObject);
    }
}