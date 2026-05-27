using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleAttackEnemy : Attackable
{
    [SerializeField] private LayerMask _layerMask;
    private Enemy _enemy;
    private BasicStatsEnemySO _basicStatsEnemySO;
    private IDamageable _target;
    private EffectController _effectController = null;
    private float _timer;
    private float _interval;
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    private void OnEnable()
    {
        _timer = Time.time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (_target != null)
        {
            return;
        }
        if (((1 << collision.gameObject.layer) & _layerMask) == 0)
        {
            return;
        }
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            _target = damageable;
        }
        _effectController = collision.gameObject.GetComponent<EffectController>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _layerMask) == 0)
            return;

        if (collision.GetComponent<IDamageable>() == _target)
            _target = null;
    }
    private void OnDisable()
    {
        _target = null;
    }
    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        if (Time.time - _timer >= _interval)
        {
            _timer = Time.time;
            DealDamage();
            if (_effectController != null)
            {
                foreach (var item in _basicStatsEnemySO.basicStats.NegativeEffectDataList)
                {
                    if (item != null)
                    {
                        _effectController.AddStatus(item);
                    }
                }
            }
        }
    }
    private void DealDamage()
    {
        if (_target == null)
            return;

        if (_enemy == null)
        {
            return;
        }

        if (_basicStatsEnemySO == null)
        {
            _basicStatsEnemySO = _enemy.BasicStatsEnemySO;

            if (_basicStatsEnemySO == null)
            {
                return;
            }
        }

        if (_basicStatsEnemySO.basicStats == null)
        {
            return;
        }

        var damageData = _basicStatsEnemySO.basicStats.DamageData;

        if (damageData == null)
        {
            return;
        }

        _target.TakeDamage(damageData);
    }
    public void ResetAttack()
    {
        _target = null;

        if (_basicStatsEnemySO == null)
        {
            _basicStatsEnemySO = _enemy.BasicStatsEnemySO;
            _interval = _basicStatsEnemySO.basicStats.IntervalBetweenAttacks;
        }

        _timer = Time.time;
    }
}
