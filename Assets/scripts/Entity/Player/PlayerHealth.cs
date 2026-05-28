using UnityEngine;
using System.Collections;
using System;
public class RegenerationData
{
    public int FlatRegen { get; private set; } = 0;
    public float MultipleRegen { get; private set; } = 0;
    public RegenerationData(int flatRegen = 0, float multipleRegen = 0)
    {
        FlatRegen = flatRegen;
        MultipleRegen = multipleRegen;
    }
}
public class PlayerHealth : MonoBehaviour, IDamageable, IChangingBar
{
    [Header("Stats")]
    [SerializeField] private float _invincibilityTime = 0.2f;
    [Header("UI")]
    [SerializeField] private BarController _hpBarController;
    private PlayerCalculateUpgrades _playerCalculateUpgrades;
    private TotalUpgradeStorage _totalUpgradeStorage;
    private PlayerBehaviour _playerBehaviour;
    private PlayerLevelSystem _playerLevelSystem;
    private SpriteRenderer _spriteRenderer;
    private int _basicHealth = 0;
    private int _health = 0;
    private int _maxHealth = 0;
    private int _resistance = 0;
    private bool _isInvincible = false;
    public event Action OnPlayerDied;
    public float _regenInterval = 1f;
    public float _regenNextTime = 0f;
    private RegenerationData _regenerationData = new();
    private void Start()
    {
        if (_hpBarController == null)
        {
            _hpBarController = GameObject.FindGameObjectWithTag("PlayerHPBar").GetComponent<BarController>();
        }
        _playerCalculateUpgrades = GetComponent<PlayerCalculateUpgrades>();
        _totalUpgradeStorage = GetComponent<TotalUpgradeStorage>();
        _playerBehaviour = GetComponent<PlayerBehaviour>();
        _playerLevelSystem = GetComponent<PlayerLevelSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _basicHealth = _playerBehaviour.PlayerBasicStatsSO.PlayerBasicStatsData.HP;
        _maxHealth = _basicHealth;
        _health = _maxHealth;
        _resistance = _playerBehaviour.PlayerBasicStatsSO.PlayerBasicStatsData.Resistance;
        _playerCalculateUpgrades.OnUpgradeCalculationFinished += UpdateConstUpgrades;
        _regenNextTime = Time.time + _regenInterval;
    }
    private void OnDestroy()
    {
        _playerCalculateUpgrades.OnUpgradeCalculationFinished -= UpdateConstUpgrades;
    }
    private void Update()
    {
        if (_regenNextTime - Time.time <= 0)
        {
            RestoreHP(_regenerationData.FlatRegen);
            RestoreHP(_regenerationData.MultipleRegen);
            _regenNextTime += _regenInterval;
        }
    }
    private void UpdateConstUpgrades()
    {
        TotalUpgrade totalUpgradeHP = _totalUpgradeStorage.GetTotalUpgrade(StatType.Health);
        UpdateHP(totalUpgradeHP);
        if(_totalUpgradeStorage.TryGetTotalUpgrade(StatType.RegenerationHP, out TotalUpgrade regenTotalUpgrade))
        {
            UpdateRegen(regenTotalUpgrade);
        }
    }
    private void UpdateHP(TotalUpgrade totalUpgrade)
    {
        float rawValue = (_basicHealth + totalUpgrade.FlatModifierTotal) * totalUpgrade.MultipleModifierTotal;
        int newMaxHealth = Mathf.RoundToInt(rawValue);
        float ratio = (float)_health / _maxHealth;
        _maxHealth = newMaxHealth;
        _health = _maxHealth;
        _health = Mathf.RoundToInt(_health * ratio);
        _totalUpgradeStorage.Debug();
        ChangeBar();
    }
    private void UpdateRegen(TotalUpgrade totalUpgrade)
    {
        _regenerationData = new RegenerationData(totalUpgrade.FlatModifierTotal, totalUpgrade.MultipleModifierTotal);
    }
    public void TakeDamage(DamageData damageData, float knockback = 0)
    {
        if (!damageData.IgnoreInvincibility)
        {
            if (_isInvincible)
            {
                return;
            }
        }

        int totalDamage = damageData.Amount - (damageData.IgnoreDefense ? 0 : _resistance);

        if (totalDamage <= 0)
        {
            return;
        }
        _isInvincible = true;
        _health -= totalDamage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        ChangeBar();

        if (_health == 0)
        {
            Death();
            return;
        }
        if (isActiveAndEnabled)
        {
            if (!damageData.IgnoreInvincibility)
            {
                StartCoroutine(Invincibility(_invincibilityTime));
            }
        }
    }
    public void RestoreHP(int hp)
    {
        _health += hp;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        ChangeBar();
    }
    public void RestoreHP(float hp) 
    {
        if (hp <= 1) return;
        _health += Mathf.RoundToInt((hp-1f) * _maxHealth);
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        ChangeBar();
    }
    private IEnumerator Invincibility(float timeSec)
    {
        yield return new WaitForSeconds(timeSec);
        _isInvincible = false;
    }
    public void ChangeBar()
    {
        _hpBarController.ChangeBar(_health, _maxHealth);
    }
    private void Death()
    {
        OnPlayerDied?.Invoke();
        Destroy(gameObject);
    }
}
