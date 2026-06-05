using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    Idle,
    Attack,
    Ability
}
public class Melee : Weapon
{
    [SerializeField] private WeaponStatsSO _weaponStatsSO;
    [SerializeField] private WeaponTransform _weaponTransform;
    private DamageData _damageData;
    private DamageData _abilityDamageData;
    [SerializeField] private MeleeAttackBehaviour _meleeAttackBehaviour;
    [SerializeField] private Animator _animator;
    protected override void Start()
    {
        base.Start();
        bool error = false;
        if (_meleeAttackBehaviour == null)
        {
            Debug.LogError($"Null reference to {nameof(_meleeAttackBehaviour)} in the script {nameof(Melee)}");
            error = true;
        }
        if (_weaponTransform == null)
        {
            _weaponTransform = GetComponent<WeaponTransform>();
        }
        if (_weaponStatsSO == null)
        {
            Debug.LogError($"Null reference to {nameof(_weaponStatsSO)} in the script {nameof(Melee)}");
            error = true;
        }
        if (_animator == null)
        {
            if (TryGetComponent(out Animator animator))
            {
                _animator = animator;
            }
            else 
            {
                Debug.LogError($"Null reference to {nameof(_animator)} in the script {nameof(Melee)}");
                error = true;
            }

        }
        if (error)
        {
            Destroy(gameObject);
            return;
        }
        SetCooldown(_weaponStatsSO.CoolDown);
        SetAbilityCooldown(_weaponStatsSO.AbilityCoolDown);
    }
    protected override void Attack()
    {
        MusicManager.instance.PlayEffect(_weaponStatsSO.AudioClip);
        _meleeAttackBehaviour.SetDamage(CalculateDamage(_weaponStatsSO.DamageData));
        _meleeAttackBehaviour.SetEffectsList(_effectsData);
        SeSetAnimatorState(WeaponState.Attack);
    }
    protected override void UseAbility()
    {
        MusicManager.instance.PlayEffect(_weaponStatsSO.AbilityAudioClip);
        _meleeAttackBehaviour.SetDamage(CalculateDamage(_weaponStatsSO.AbilityDamageData));
        _meleeAttackBehaviour.SetEffectsList(_effectsData);
        SeSetAnimatorState(WeaponState.Ability);
    }
    public void EnableAttackCollider() {
        _meleeAttackBehaviour.EnableCollider(true);
    }
    public void DisableAttackCollider(WeaponState weaponState) {
        _meleeAttackBehaviour.EnableCollider(false);
    }
    public void SeSetAnimatorState(WeaponState weaponState)
    {
        _animator.SetInteger("AttackState", (int)weaponState);
    }
}
