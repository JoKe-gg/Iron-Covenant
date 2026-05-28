using System.Collections.Generic;
using UnityEngine;

public class Shooting : Weapon
{
    [SerializeField] private WeaponStatsSO _weaponStatsSO;
    [SerializeField] private PlayerCalculateUpgrades _playerCalculateUpgrades;
    [SerializeField] private Transform _anchoredPosition;
    [SerializeField] private WeaponTransform _weaponTransform;
    private SpriteRenderer _spriteRenderer;
    private ProjectilePool _projectilePool;
    private ProjectilePool _abilityProjectilePool;
    private TotalUpgrade _damageUpgrade;
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        bool error = false;
        _projectilePool = GameManager.instance.ProjectilePool;
        _abilityProjectilePool = GameManager.instance.AbilityProjectilePool;
        if (_projectilePool == null)
        {
            Debug.LogError($"Null reference to {nameof(_projectilePool)} in the script {nameof(Shooting)}");
            error = true;
        }
        if (_abilityProjectilePool == null)
        {
            Debug.LogError($"Null reference to {nameof(_abilityProjectilePool)} in the script {nameof(Shooting)}");
            error = true;
        }
        if (_weaponTransform == null)
        {
            _weaponTransform = GetComponent<WeaponTransform>();
        }
        if (_playerCalculateUpgrades == null) 
        {
            Debug.LogError($"Null reference to {nameof(_playerCalculateUpgrades)} in the script {nameof(Shooting)}");
            error = true;
        }
        if (_totalUpgradeStorage == null)
        {
            Debug.LogError($"Null reference to {nameof(_totalUpgradeStorage)} in the script {nameof(Shooting)}");
            error = true;
        }
        if (_weaponStatsSO == null)
        {
            Debug.LogError($"Null reference to {nameof(_weaponStatsSO)} in the script {nameof(Shooting)}");
            error = true;
        }
        if (_anchoredPosition == null)
        {
            Debug.LogError($"Null reference to {nameof(_anchoredPosition)} in the script {nameof(Shooting)}");
            error = true;
        }
        if (error)
        {
            Destroy(gameObject);
            return;
        }
        SetCooldown(_weaponStatsSO.CoolDown);
        SetAbilityCooldown(_weaponStatsSO.AbilityCoolDown);
        _playerCalculateUpgrades.OnUpgradeCalculationFinished += UpdateUpgrade;
        UpdateUpgrade();
    }
    private void OnDestroy()
    {
        _playerCalculateUpgrades.OnUpgradeCalculationFinished -= UpdateUpgrade;
    }
    protected override void Attack()
    {
        MusicManager.instance.PlayEffect(_weaponStatsSO.AudioClip);
        DamageData _basicDamageData = _weaponStatsSO.DamageData;
        DamageData damage = GetDamage(_basicDamageData);
        float speed = _weaponStatsSO.Speed * 2f * (_weaponTransform.IsFlipped() ? -1 : 1);
        bool flipX = _spriteRenderer.flipX;
        BulletBehaviour bulletBehaviour = _projectilePool.GetProjectile();
        bulletBehaviour.transform.localRotation = transform.localRotation; 
        bulletBehaviour.Initialize(_effectsData, gameObject, _anchoredPosition.position, transform, damage, _weaponStatsSO.Penetration, speed, _projectilePool, flipX, 2f);
    }
    protected override void UseAbility()
    {
        MusicManager.instance.PlayEffect(_weaponStatsSO.AudioClip);
        DamageData _basicDamageData = _weaponStatsSO.AbilityDamageData;
        DamageData damage = GetDamage(_basicDamageData);
        float speed = _weaponStatsSO.AbilitySpeed * 2f * (_weaponTransform.IsFlipped() ? -1 : 1);
        bool flipX = _spriteRenderer.flipX;
        BulletBehaviour bulletBehaviour = _abilityProjectilePool.GetProjectile();
        bulletBehaviour.transform.localRotation = transform.localRotation;
        bulletBehaviour.Initialize(_effectsData, gameObject, _anchoredPosition.position, transform, damage, _weaponStatsSO.AbilityPenetration, speed, _abilityProjectilePool, flipX, 5f);
    }
    private void UpdateUpgrade()
    {
        _damageUpgrade = _totalUpgradeStorage.GetTotalUpgrade(StatType.Damage);
        if (_damageUpgrade == null)
        {
            Debug.LogWarning("Damage upgrade not ready yet");
            return;
        }
    }
    private DamageData GetDamage(DamageData damage)
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
        damage = new DamageData(Mathf.RoundToInt((amountOfDamage + _damageUpgrade.FlatModifierTotal) * _damageUpgrade.MultipleModifierTotal), damage.DamageType, _playerCalculateUpgrades.gameObject);
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
}
