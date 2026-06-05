using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private ProjectilePool _projectilePoolPrefab;
    [SerializeField] private GemPool _gemPool;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private BossBarController _bossBarController;
    [SerializeField] private DamageDisplayPool _DamageDisplayPool;
    public GemPool GemPool => _gemPool;
    public EnemyPool EnemyPool => _enemyPool;
    public BossBarController BossBarController => _bossBarController;
    public DamageDisplayPool DamageDisplayPool => _DamageDisplayPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public ProjectilePool InitializeProjectilePool(Projectile projectile, int startAmount, int maxAmount)
    {
        ProjectilePool projectilePool = Instantiate(_projectilePoolPrefab, transform);
        projectilePool.Initialize(projectile, startAmount, maxAmount);
        return projectilePool;
    }
}
