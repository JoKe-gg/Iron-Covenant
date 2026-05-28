using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Drop prefab")]
    [SerializeField] private GameObject _expGem;
    [Header("Taking damage settings")]
    [SerializeField] private float _invincibilityTime = 0.12f;
    [Header("UI")]
    [SerializeField] private TakingDamageDisplay _damageDisplay;
    private BasicStatsEnemySO _basicStatsEnemySO;
    private Enemy _enemy;
    private EnemyMovement _enemyMovement;
    private GemPool _gemPool;
    private int _health;
    private int _maxHealth;
    private int _resistance;
    private bool _isAbleToTakeDamage = true;
    private bool _isDead = false;
    public event Action<int, int> OnHealthChanged;
    private Vector2 _bossDisplayScale = new Vector2(2, 2);
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }
    void Start()
    {
        bool error = false;
        if (_expGem == null)
        {
            Debug.LogError($"Null reference to {nameof(_expGem)} in the script {nameof(EnemyHealth)}");
            error = true;
        }
        if (error)
        {
            Destroy(gameObject);
            return;
        }  
        _gemPool = GameManager.instance.GemPool;
        _basicStatsEnemySO = _enemy.BasicStatsEnemySO;
        _maxHealth = _basicStatsEnemySO.basicStats.BasicMaxHealth;
        _resistance = _basicStatsEnemySO.basicStats.BasicResist;
        _health = _maxHealth;
        _basicStatsEnemySO = _enemy.BasicStatsEnemySO;
    }
    private void OnEnable()
    {
        if (StateManager.instance != null)
            StateManager.instance.OnStateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        if (StateManager.instance != null)
            StateManager.instance.OnStateChanged -= OnStateChanged;
        _isAbleToTakeDamage = true;
    }
    private void OnStateChanged(RuntimeState state)
    {
        switch (state)
        {
            case RuntimeState.Victory:
                {
                    _isDead = true;
                    StopAllCoroutines();
                    if (!TryGetComponent(out BossBehaviour bossBehavioe))
                    {
                        _enemy.ReturnToPool();
                    }
                    break;
                }
            default:
                break;
        }
    }
    public void TakeDamage(DamageData damageData, float knockback = 0)
    {
        TakeDamageLogic(damageData);
        if (knockback > 0)
        _enemyMovement.KnockBack(knockback);
    }
    private void TakeDamageLogic(DamageData damageData)
    {
        if (_isDead) 
        {
            return; 
        }
        if (!damageData.IgnoreInvincibility)
        {
            if (!_isAbleToTakeDamage)
            {
                return;
            }
        }

        int totalDamage = damageData.Amount - (damageData.IgnoreDefense ? 0 : _resistance);

        if (totalDamage <= 0)
        {
            return;
        }
        _health -= totalDamage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        OnHealthChanged?.Invoke(_health, _maxHealth);

        if (_health == 0)
        {
            Death();
            return;
        }
        Color color = Color.black;
        if (_damageDisplay != null)
        {
            switch (damageData.DamageType)
            {
                case DamageType.Poison:
                    color = Color.darkGreen; 
                    break;
                case DamageType.Fire:
                    color = Color.orange;
                    break;
                case DamageType.Physical:
                    ColorUtility.TryParseHtmlString("#671212", out Color physColor);
                    color = physColor;
                    break;
                default:
                    break;
            }
            Vector2 localScale = _basicStatsEnemySO.basicStats.EnemyType == EnemyType.Boss
                ? _bossDisplayScale 
                : Vector2.one;
            _damageDisplay.DisplayDamage(totalDamage, color, localScale);
            
        }
        if (isActiveAndEnabled)
        {
            if (!damageData.IgnoreInvincibility)
            {
            StartCoroutine(InvincibilityFrames());
            }
        }
    }
    private IEnumerator InvincibilityFrames()
    {
        _isAbleToTakeDamage = false;
        yield return new WaitForSeconds(_invincibilityTime);
        _isAbleToTakeDamage = true;
    }
    public void RestoreHP(int healing) 
    {
        _health += healing;
        _health = Math.Clamp(_health, 0, _maxHealth);
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }
    public void RestoreHP(float percent)
    {
        _health += (int)((float)_maxHealth * percent);
        _health = Math.Clamp(_health, 0, _maxHealth); 
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }
    public void UpdateHP()
    {
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }
    public void Death()
    {
        if (_isDead)
            return;
        RuntimeStats.Instance.AddKilledEnemy(_basicStatsEnemySO.name);
        _isDead = true;
        StopAllCoroutines();
        DropGem();
        AccrueCoins();
        if (TryGetComponent(out BossBehaviour bossBehaviour))
        {
            bossBehaviour.OnBossDied();
        }
        else
        {
            _enemy.ReturnToPool();
        }
    }
    public void ResetHealth()
    {
        _isDead = false;
        _maxHealth = _basicStatsEnemySO.basicStats.BasicMaxHealth;
        RestoreHP(1f);
    }
    private void DropGem()
    {
        if (_basicStatsEnemySO != null)
        {
            Vector2 ExpRange = _basicStatsEnemySO.basicStats.ExpForKillingRange;
            int ExpForKilling = Mathf.RoundToInt(UnityEngine.Random.Range(ExpRange.x, ExpRange.y));
            Gem gem = _gemPool.GetGem();
            if (gem != null)
            {
                gem.Initialize(ExpForKilling, transform.position, _gemPool);
            }
        }
    }
    private void AccrueCoins()
    {
        if (_basicStatsEnemySO != null)
        {
            Vector2 CoinsRange = _basicStatsEnemySO.basicStats.CoinsForKillingRange;
            int CoinsForKilling = Mathf.RoundToInt(UnityEngine.Random.Range(CoinsRange.x, CoinsRange.y));
            CoinsManager.instance.AddCoins(CoinsForKilling);
        }
    }
}
