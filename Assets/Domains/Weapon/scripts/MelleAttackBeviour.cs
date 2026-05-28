using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class MeleeAttackBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Collider2D _collider;

    private DamageData _damageData = null;
    private List<NegativeEffectData> _effectsList;

    private void Awake()
    {
        if (_collider == null)
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        if (_collider == null)
        {
            Debug.LogError($"No {nameof(BoxCollider2D)} found on {gameObject.name}");
            enabled = false;
            return;
        }

        EnableCollider(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _layerMask) == 0)
            return;

        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damageData, 4f);
            if (collision.TryGetComponent(out EffectController effectController))
            {
                foreach (var effect in _effectsList) 
                {
                    effectController.AddStatus(effect);
                }
            }
        }
    }
    public void SetDamage(DamageData NewDamageData)
    {
        _damageData = NewDamageData;
    }
    public void SetEffectsList(List<NegativeEffectData> negativeEffectsData) => _effectsList = negativeEffectsData;
    public void EnableCollider(bool value = true)
    {
        _collider.enabled = value;
    }
}