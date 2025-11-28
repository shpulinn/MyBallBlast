using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [Space] [SerializeField] private int damage;
    [SerializeField] private float fireRate;
    [SerializeField] private int projectileAmount;
    [SerializeField] private float projectileInterval;
    
    public UnityEvent OnFire;

    private float timer;
    
    public int Damage => damage;
    public int ProjectileAmount => projectileAmount;
    public float FireRate => fireRate;

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Fire()
    {
        if (timer >= fireRate)
        {
            SpawnProjectile();
            timer = 0;
        }
    }

    private void SpawnProjectile()
    {
        float startPosX = firePoint.position.x - projectileInterval * (projectileAmount  -1) * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Projectile projectile = Instantiate(
                projectilePrefab, 
                new Vector3(startPosX + i * projectileInterval, firePoint.position.y, firePoint.position.z), 
                transform.rotation
            );
            projectile.SetDamage(damage);
        }
        SoundManager.PlaySound(SoundType.Shoot);
        OnFire?.Invoke();
    }
}
