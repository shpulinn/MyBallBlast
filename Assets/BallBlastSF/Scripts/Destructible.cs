using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    public int maxHealth;
    
    public UnityEvent OnDeath;
    public UnityEvent ChangeHealth;
    private int health;
    private bool isDead = false;
    
    private void Start()
    {
        health = maxHealth;
        ChangeHealth?.Invoke();
    }

    public void Reset()
    {
        health = maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        
        ChangeHealth?.Invoke();

        if (health <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        if (isDead) return;
        
        health = 0;
        isDead = true;
        
        ChangeHealth?.Invoke();
        OnDeath?.Invoke();
    }

    public int GetHealth()
    {
        return health;
    }
}
