using System.Collections;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    private ObjectPool<Projectile> _pool = null;

    public void Initialize(Projectile prefab, int startCount, int maxProjectiles)
    {
        _pool = new ObjectPool<Projectile>(prefab, startCount, transform, maxProjectiles);
    }
    public Projectile GetProjectile()
    {
        return _pool.Get();
    }

    public void ReturnProjectile(Projectile projectile)
    {
        _pool.Return(projectile);
    }
}
