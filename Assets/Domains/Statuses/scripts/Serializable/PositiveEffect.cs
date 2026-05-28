using System;
using UnityEngine;

[Serializable]
public class PositiveEffectData
{
    [Header("Status")]
    public bool IsStatus;
    public float TimeOfEffect;
    public float IntervalBetweenTicks;
    public DamageData DamageData;
    public PositiveEffectData(int amount, DamageType damageType, GameObject source = null,
        bool ignoreDefense = false,
        bool ignoreInvincibility = false,
        bool isStatus = false,
        float timeOfEffect = 0f,
        float intervalBetweenTicks = 0.1f)
    {
        DamageData.Amount = amount;
        DamageData.DamageType = damageType;
        DamageData.Source = source;
        DamageData.IgnoreDefense = ignoreDefense;
        DamageData.IgnoreInvincibility = ignoreInvincibility;

        IntervalBetweenTicks = intervalBetweenTicks;
        TimeOfEffect = timeOfEffect;
        IntervalBetweenTicks = intervalBetweenTicks;
    }
}
