using UnityEngine;

enum WeaponType 
{
    melee,
    range
}

[CreateAssetMenu(fileName = "WeaponStatsSO", menuName = "Scriptable Objects/WeaponStatsSO")]
public class WeaponStatsSO : ScriptableObject
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private string _weaponName;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _abilityProjectileSpeed;
    [SerializeField] private DamageData _damageData;
    [SerializeField] private DamageData _abilityDamageData;
    [SerializeField] private float _coolDown;
    [SerializeField] private float _abilityCoolDown;
    [SerializeField] private int _penetration;
    [SerializeField] private int _abilityPenetration;
    [SerializeField] private float _speed;
    [SerializeField] private float _abilitySpeed;
    [SerializeField] private AudioClip _audioClip;

    public int WeaponType => (int)_weaponType;
    public string WeaponTypeName => _weaponType.ToString();
    public string WeaponName => _weaponName;
    public float ProjectileSpeed => _projectileSpeed;
    public float AbilityProjectileSpeed => _abilityProjectileSpeed;
    public DamageData DamageData => _damageData;
    public DamageData AbilityDamageData => _abilityDamageData;
    public float CoolDown => _coolDown;
    public float AbilityCoolDown => _abilityCoolDown;
    public int Penetration => _penetration;
    public int AbilityPenetration => _abilityPenetration;
    public float Speed => _speed;
    public float AbilitySpeed => _abilitySpeed;
    public AudioClip AudioClip => _audioClip;
}
