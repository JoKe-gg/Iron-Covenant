using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(WeaponTransform))]
public abstract class Weapon : MonoBehaviour
{
    protected TotalUpgradeStorage  _totalUpgradeStorage;
    private bool _isAbleToAttack = true;
    private bool _isAbleToUseAbility = true;
    protected float _cooldown = 0.2f;
    protected float _abilityCooldown = 3f;
    [SerializeField] protected List<NegativeEffectData> _effectsData = new();
    protected LevelUPUpgradeData _damagePermanentUpgrade;
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
}
