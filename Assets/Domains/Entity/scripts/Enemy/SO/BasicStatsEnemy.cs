using System;
using UnityEngine;
using System.Collections.Generic;

public enum BossRegardType
{
    NewPlayer,
    NewMap,
}
[Serializable]
public class BossRegardData
{
    [SerializeField] private List<BossRegardType> _regards = new();
    [SerializeField] private int _playerId;
    [SerializeField] private int _mapId;
    public List<BossRegardType> Regards => _regards;
    public int PlayerId => _playerId;
    public int MapId => _mapId;
    BossRegardData()
    {
        _playerId = 0;
        _mapId = 0;
    }
}
public enum EnemyType
{
    Regular,
    Boss,
}
[Serializable]
public class BasicStatsEnemy
{
    [Header("ID")]
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private int _basicMaxHealth = 100;
    [SerializeField] private int _basicResist = 0;
    [Header("Death regard")]
    [SerializeField] private Vector2Int _expForKillingRange = new Vector2Int(5, 10);
    [SerializeField] private Vector2Int _coinsForKillingRange = new Vector2Int(2, 8);
    [SerializeField] private BossRegardData _data;
    [SerializeField] private DamageData _basicDamageData;
    [SerializeField] private List<NegativeEffectData> _negativeEffectData;
    [SerializeField] private float _basicMovementSpeed = 2;
    [SerializeField] private float _intervalBetweenAttacks = 0.1f;

    public EnemyType EnemyType => _enemyType;
    public int BasicMaxHealth => _basicMaxHealth;
    public int BasicResist => _basicResist;
    public Vector2Int ExpForKillingRange => _expForKillingRange;
    public Vector2Int CoinsForKillingRange => _coinsForKillingRange;
    public BossRegardData Data => _data;
    public DamageData DamageData => _basicDamageData; 
    public List<NegativeEffectData> NegativeEffectDataList => _negativeEffectData;
    public float BasicMovementSpeed => _basicMovementSpeed;
    public float IntervalBetweenAttacks => _intervalBetweenAttacks;

    public void OnValidate()
    {
        switch (_enemyType)
        {
            case EnemyType.Boss:
                break;
            case EnemyType.Regular:
                _data = null;
                break;
            default:
                break;
        }

        if (_expForKillingRange.x < 0){
            _expForKillingRange.x = 0;
        }
        if (_expForKillingRange.y < 0){
            _expForKillingRange.y = 0;
        }
        if (_expForKillingRange.x > _expForKillingRange.y){
            _expForKillingRange.y = _expForKillingRange.x + 1;
        }

        if (_coinsForKillingRange.x < 0){
            _coinsForKillingRange.x = 0;
        }
        if (_coinsForKillingRange.y < 0){
            _coinsForKillingRange.y = 0;
        }
        if (_coinsForKillingRange.x > _coinsForKillingRange.y)
        {
            _coinsForKillingRange.y = _coinsForKillingRange.x + 1;
        }

        if (_basicMaxHealth < 1){
            _basicMaxHealth = 1;
        }
        if (_basicResist < 0){
            _basicResist = 0;
        }
        if (_basicMovementSpeed < 0.1f)
        {
            _basicMovementSpeed = 0.1f;
        }
        
    }
}

