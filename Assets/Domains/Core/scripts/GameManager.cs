using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private ProjectilePool _projectilePool;
    [SerializeField] private ProjectilePool _abilityProjectilePool;
    [SerializeField] private GemPool _gemPool;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private BossBarController _bossBarController;
    [SerializeField] private DamageDisplayPool _DamageDisplayPool;
    public ProjectilePool ProjectilePool => _projectilePool;
    public ProjectilePool AbilityProjectilePool => _abilityProjectilePool;
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
}
