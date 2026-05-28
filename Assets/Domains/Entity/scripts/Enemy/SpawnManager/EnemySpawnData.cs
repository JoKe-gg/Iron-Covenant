using JetBrains.Annotations;
using System;
using UnityEngine;

public enum EnemySpawnType
{
    Regular,
    MiniBoss,
    Boss
}
[Serializable]
public class EnemySpawnData
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Vector2 _timeInterval;
    [SerializeField] private string _enemyName;
    [SerializeField] private EnemySpawnType _enemySpawnType = EnemySpawnType.Regular;
    public Enemy EnemyPrefab => _enemyPrefab;
    public Vector2 TimeInterval => _timeInterval;
    public string EnemyName => _enemyName;
    public EnemySpawnType EnemySpawnType => _enemySpawnType;

    public Vector2 GetIntervalToSeconds()
    {
        return new Vector2(_timeInterval.x, _timeInterval.y) * 60;
    }
    public void OnValidate()
    {
        if (_timeInterval.x > _timeInterval.y)
        {
            _timeInterval.y = _timeInterval.x + 0.1f;
        }
    }
}
