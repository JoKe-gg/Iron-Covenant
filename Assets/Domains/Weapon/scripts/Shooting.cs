using System.Collections.Generic;
using UnityEngine;

public class Shooting : Weapon
{
    [Header("Components")]
    [SerializeField] private WeaponTransform _weaponTransform;
    [Header("Weapon stats")]
    [SerializeField] private WeaponStatsSO _weaponStatsSO;
    [Header("Projectiles")]
    [SerializeField] private RegularProjectileBehaviour _abilityProjectile;
    [SerializeField] private RegularProjectileBehaviour _regularProjectile;
    [Header("Anchored transform")]
    [SerializeField] private Transform _anchoredPosition;
    private ProjectilePool _projectilePool;
    private ProjectilePool _abilityProjectilePool;
    private TotalUpgrade _damageUpgrade;
    protected override void Awake()
    {
        base.Awake();
        bool error = false;
        if (_weaponTransform == null)
        {
            _weaponTransform = GetComponent<WeaponTransform>();
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
    }
    protected override void Start()
    {

        base.Start();
        if (GameManager.instance != null)
        {
            _projectilePool = GameManager.instance.InitializeProjectilePool(_regularProjectile, 20, 200);
            _abilityProjectilePool = GameManager.instance.InitializeProjectilePool(_abilityProjectile, 3, 20);
        }
        else
        {
            Debug.LogError($"{nameof(GameManager.instance)} not exists in class {nameof(Spelling)}", this);
            Destroy(gameObject);
            return;
        }
    }
    protected override void Attack()
    {
        MusicManager.instance.PlayEffect(_weaponStatsSO.AudioClip);
        DamageData _basicDamageData = _weaponStatsSO.DamageData;
        DamageData damage = CalculateDamage(_basicDamageData);
        float speed = _weaponStatsSO.Speed * 2f * (_weaponTransform.IsFlipped() ? -1 : 1);
        bool flipX = _weaponTransform.IsFlipped();
        RegularProjectileBehaviour bulletBehaviour = _projectilePool.GetProjectile().GetComponent<RegularProjectileBehaviour>();
        bulletBehaviour.transform.localRotation = transform.localRotation; 
        bulletBehaviour.Initialize(_effectsData, gameObject, _anchoredPosition.position, transform, damage, _weaponStatsSO.Penetration, speed, _projectilePool, flipX, 2f);
    }
    protected override void UseAbility()
    {
        MusicManager.instance.PlayEffect(_weaponStatsSO.AbilityAudioClip);
        DamageData _basicDamageData = _weaponStatsSO.AbilityDamageData;
        DamageData damage = CalculateDamage(_basicDamageData);
        float speed = _weaponStatsSO.AbilitySpeed * 2f * (_weaponTransform.IsFlipped() ? -1 : 1);
        bool flipX = _weaponTransform.IsFlipped();
        RegularProjectileBehaviour bulletBehaviour = _abilityProjectilePool.GetProjectile().GetComponent<RegularProjectileBehaviour>();
        bulletBehaviour.transform.localRotation = transform.localRotation;
        bulletBehaviour.Initialize(_effectsData, gameObject, _anchoredPosition.position, transform, damage, _weaponStatsSO.AbilityPenetration, speed, _abilityProjectilePool, flipX, 5f);
    }
}
