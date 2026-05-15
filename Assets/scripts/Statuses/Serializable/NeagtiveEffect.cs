using System;
using UnityEngine;

public enum StatusEffectType
{
    Poison,
    Burn,
    Stun,
    Slow
}
[Serializable]
public class NegativeEffectData
{
    [Header("Status")]
    [SerializeField] private StatusEffectType _effectType;
    [SerializeField] private float _timeOfEffect;
    [SerializeField] private float _intervalBetweenTicks;
    [SerializeField] private DamageData _damageData;

    public StatusEffectType EffectType => _effectType;
    public float TimeOfEffect => _timeOfEffect;
    public float IntervalBetweenTicks => _intervalBetweenTicks;
    public DamageData DamageData => _damageData;
    public NegativeEffectData(int amount, DamageType damageType, StatusEffectType statusEffectType, int level,
        GameObject source = null,
        bool ignoreDefense = false,
        bool ignoreInvincibility = false,
        float timeOfEffect = 0f,
        float intervalBetweenTicks = 0.1f)
    {
        DamageData.Amount = amount;
        DamageData.DamageType = damageType;
        DamageData.Source = source;
        DamageData.IgnoreDefense = ignoreDefense;
        DamageData.IgnoreInvincibility = ignoreInvincibility;
        _effectType = statusEffectType;
        _intervalBetweenTicks = intervalBetweenTicks;
        _timeOfEffect = timeOfEffect;
    }
}
