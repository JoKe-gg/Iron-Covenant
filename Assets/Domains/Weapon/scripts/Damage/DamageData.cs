using System;
using UnityEngine;
public enum DamageType
{
    Physical,
    Poison,
    Fire,
    Ice,
    Lightning,
    True
}

[Serializable]
public class DamageData
{
    [Header("Basic damage data")]
    public int Amount;
    public DamageType DamageType;
    public GameObject Source;
    public bool IgnoreDefense;
    public bool IgnoreInvincibility;
    public DamageData(
        int amount, 
        DamageType damageType, 
        GameObject source = null,
        bool ignoreDefense = false,
        bool ignoreInvincibility = false)
    {
        Amount = amount;
        DamageType = damageType;
        Source = source;
        IgnoreDefense = ignoreDefense;
        IgnoreInvincibility = ignoreInvincibility;
    }
}
