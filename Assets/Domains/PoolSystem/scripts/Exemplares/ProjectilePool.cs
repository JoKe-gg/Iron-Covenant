using System.Collections;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private BulletBehaviour _prefab;
    [SerializeField] private int _maxProjectiles = 300;
    private ObjectPool<BulletBehaviour> _pool;

    [SerializeField] private int _startCount = 0;
    private void Awake()
    {
        _pool = new ObjectPool<BulletBehaviour>(_prefab, _startCount, transform, _maxProjectiles);
    }
    public BulletBehaviour GetProjectile()
    {
        return _pool.Get();;
    }

    public void ReturnProjectile(BulletBehaviour projectile)
    {
        _pool.Return(projectile);
    }
}
