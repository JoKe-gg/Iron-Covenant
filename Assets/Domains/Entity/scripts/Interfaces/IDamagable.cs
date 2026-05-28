using UnityEngine;
using System.Collections;
using System;

public interface IDamageable
{
    public abstract void TakeDamage(DamageData damageData, float knockback = 0);
    public abstract void RestoreHP(int intHeal);
    public abstract void RestoreHP(float percent);
}
