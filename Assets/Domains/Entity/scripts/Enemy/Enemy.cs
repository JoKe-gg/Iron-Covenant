using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EnemyBrain))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EffectController))]
[RequireComponent(typeof(EnemySensor))]
public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private BasicStatsEnemySO _basicStatsEnemySO;
    //Components
    public BasicStatsEnemySO BasicStatsEnemySO => _basicStatsEnemySO;
    private EnemyHealth _enemyHealth;
    private EnemyMovement _enemyMovement;
    private MeleAttackEnemy _meleAttackEnemy;
    private EnemyBrain _enemyBrain;
    private EnemyAnimator _enemyAnimator;
    private EffectController _effectController;
    private EnemySensor _enemySensor;
    //Pool
    private ObjectPool<Enemy> _enemyPool;
    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _effectController = GetComponent<EffectController>();
        _meleAttackEnemy = GetComponentInChildren<MeleAttackEnemy>();
        _enemyBrain = GetComponent<EnemyBrain>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemySensor = GetComponent<EnemySensor>();
    }
    public void OnSpawnFromPool()
    {
        _effectController.ResetEffects();
    }
    public void OnReturnToPool()
    {
        TotalReset();
    }
    private void TotalReset()
    {
        _enemyHealth.ResetHealth();
        _enemyMovement.ResetMovement();
        _effectController.ResetEffects();
        _meleAttackEnemy.ResetAttack();
    }
    public void Initialize(ObjectPool<Enemy> enemyPool, Vector2 pos)
    {
        transform.position = pos;
        _enemyPool = enemyPool;
    }
    public void ReturnToPool()
    {
        if (_enemyPool != null)
        {
            _enemyPool.Return(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
