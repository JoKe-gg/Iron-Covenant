using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponTransform))]
public abstract class Weapon : MonoBehaviour
{
    [Header("Player components")]
    [SerializeField] private PlayerCalculateUpgrades _playerCalculateUpgrades;
    [Header("Effects")]
    [SerializeField] protected List<NegativeEffectData> _effectsData = new();
    protected TotalUpgradeStorage  _totalUpgradeStorage;
    private bool _isAbleToAttack = true;
    private bool _isAbleToUseAbility = true;
    protected float _cooldown = 0.2f;
    protected float _abilityCooldown = 3f;
    protected LevelUPUpgradeData _damagePermanentUpgrade;
    private TotalUpgrade _totalUpgrade;
    protected virtual void Awake()
    {
        _totalUpgradeStorage = GetComponentInParent<TotalUpgradeStorage>();
        if (_totalUpgradeStorage == null)
        {
            Debug.LogError($"Null reference to {nameof(_totalUpgradeStorage)} in the script {nameof(Weapon)}");
            return;
        }
    }
    protected virtual void Start()
    {
        bool error = false;
        if (_playerCalculateUpgrades == null)
        {
            Debug.LogError($"Null reference to {nameof(_playerCalculateUpgrades)} in the script {nameof(Melee)}");
            error = true;
        }
        if (error)
        {
            Destroy(gameObject);
            return;
        }
        _playerCalculateUpgrades.OnUpgradeCalculationFinished += UpdateUpgrade;
        UpdateUpgrade();
    }
    private void OnDestroy()
    {
        if (_playerCalculateUpgrades != null)
        {
            _playerCalculateUpgrades.OnUpgradeCalculationFinished -= UpdateUpgrade;
        }
    }
    protected virtual void OnEnable()
    {
        if (_totalUpgradeStorage != null)
        {
            _totalUpgradeStorage.OnEffectListChanged += ReCalculateEffects;
        }
    }
    protected virtual void OnDisable()
    {
        if (_totalUpgradeStorage != null)
        {
            _totalUpgradeStorage.OnEffectListChanged -= ReCalculateEffects;
        }
    }
    public void TryAttack()
    {
        if (!_isAbleToAttack )
            return;

        Attack();
        StartCoroutine(CooldownBetweenShoots(_cooldown));
    }
    public void TryAbilityAttack()
    {
        if (!_isAbleToUseAbility)
            return;

        UseAbility();
        StartCoroutine(AbilityCooldownBetweenShoots(_abilityCooldown));
    }
    private IEnumerator CooldownBetweenShoots(float coolDown)
    {
        _isAbleToAttack = false;
        yield return new WaitForSeconds(coolDown);
        _isAbleToAttack = true;
    }
    private IEnumerator AbilityCooldownBetweenShoots(float coolDown)
    {
        _isAbleToUseAbility = false;
        yield return new WaitForSeconds(coolDown);
        _isAbleToUseAbility = true;
    }
    public void SetCooldown(float newCoolDown)
    {
        _cooldown = newCoolDown;
    }
    public void SetAbilityCooldown(float newAbilityCooldown)
    {
        _abilityCooldown = newAbilityCooldown;
    }
    public void ReCalculateEffects(List<Effect> list)
    {
        _effectsData.Clear();
        foreach (Effect effect in list)
        {
            if (effect != null && effect.EffectData != null)
                _effectsData.Add(effect.EffectData);
        }
    }
    public void RemoveAllEffects()
    {
        _effectsData.Clear();
    }
    protected abstract void Attack();
    protected abstract void UseAbility();

    protected DamageData CalculateDamage(DamageData damage)
    {
        int amountOfDamage = damage.Amount;
        List<int> flatModifiers = new List<int>();
        List<float> multipleModifiers = new List<float>();
        int totalFlat = 0;
        float totalMultiple = 1;
        if (_damagePermanentUpgrade != null)
        {
            var statModifierDatas = _damagePermanentUpgrade.StatModifierData;

            foreach (var statModifierData in statModifierDatas)
            {
                switch (statModifierData.StatModifierType)
                {
                    case StatModifierType.Flat:
                        flatModifiers.Add(Mathf.FloorToInt(statModifierData.Value));
                        break;
                    case StatModifierType.Multiple:
                        multipleModifiers.Add(statModifierData.Value);
                        break;
                    default:
                        break;
                }
            }
            totalFlat = CalculateFlat(flatModifiers);
            totalMultiple = CalculateMultiple(multipleModifiers);
        }
        amountOfDamage = Mathf.RoundToInt((amountOfDamage + totalFlat) * totalMultiple);
        damage = new DamageData(Mathf.RoundToInt((amountOfDamage + _totalUpgrade.FlatModifierTotal) * _totalUpgrade.MultipleModifierTotal), damage.DamageType, _playerCalculateUpgrades.gameObject);
        return damage;
    }
    private int CalculateFlat(List<int> flatModifiers)
    {
        int totalFlat = 0;
        foreach (int modifier in flatModifiers)
        {
            totalFlat += modifier;
        }
        return totalFlat;
    }
    private float CalculateMultiple(List<float> multipleModifiers)
    {
        float totalMultiple = 1;
        foreach (float modifier in multipleModifiers)
        {
            if (modifier > 0)
            {
                totalMultiple *= modifier;
            }
        }
        return totalMultiple;
    }
    private void UpdateUpgrade()
    {
        _totalUpgrade = _totalUpgradeStorage.GetTotalUpgrade(StatType.Damage);
        if (_totalUpgrade == null)
        {
            Debug.LogWarning("Damage upgrade not ready yet");
            return;
        }
    }
}
