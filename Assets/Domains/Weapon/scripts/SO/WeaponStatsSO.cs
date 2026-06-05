using UnityEngine;

public enum WeaponType 
{
    melee,
    range
}

[CreateAssetMenu(fileName = "WeaponStatsSO", menuName = "Scriptable Objects/WeaponStatsSO")]
public class WeaponStatsSO : ScriptableObject
{
    [Header("Id")]
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private string _weaponName;
    [Header("Regular")]
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private DamageData _damageData;
    [SerializeField] private float _coolDown;
    [SerializeField] private int _penetration;
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _audioClip;
    [Header("Ability")]
    [SerializeField] private float _abilityProjectileSpeed;
    [SerializeField] private DamageData _abilityDamageData;
    [SerializeField] private float _abilityCoolDown;
    [SerializeField] private int _abilityPenetration;
    [SerializeField] private float _abilitySpeed;
    [SerializeField] private AudioClip _abilityAudioClip;

    public int WeaponTypeInt => (int)_weaponType;
    public WeaponType WeaponType => _weaponType;
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
    public AudioClip AbilityAudioClip => _abilityAudioClip;
}
