using UnityEngine;
using System.Collections.Generic;
public class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemySpawnSO _enemySpawnSO;
    [SerializeField] private int _startCount = 20;
    [SerializeField] private int _maxSize = 150;
    private List<ObjectPool<Enemy>> _pool = new();
    private List<int> _indexes = new();
    private float Timer => GameTimer.Instance.ElapsedTime;

    private void Start()
    {
        foreach (var item in _enemySpawnSO.EnemySpawnDatas)
        {
            if (item.EnemySpawnType == EnemySpawnType.Boss)
            {
                ObjectPool<Enemy> bossObjectPool = new(item.EnemyPrefab, 1, transform, 1);
                _pool.Add(bossObjectPool);
                continue;
            }
            ObjectPool<Enemy> objectPool = new(item.EnemyPrefab, _startCount, transform, _maxSize);
            _pool.Add(objectPool);
        }
    }
    public Enemy GetEnemy(out ObjectPool<Enemy> pool)
    {
        pool = null;
        if (_pool == null || _pool.Count == 0)
        {
            return null;
        }
        SetAvailablePoolIndexes();
        if (_indexes == null || _indexes.Count == 0)
        {
            return null;
        }
        List<ObjectPool<Enemy>> poolList = new();

        int randomIndex = Random.Range(0, _indexes.Count);
        int poolIndex = _indexes[randomIndex];
        pool = _pool[poolIndex];
        return pool.Get();
    }

    private void SetAvailablePoolIndexes()
    {
        _indexes.Clear();
        if (_pool != null && _pool.Count > 0)
        {
            for (int i = 0; i < _enemySpawnSO.EnemySpawnDatas.Count; ++i) {
                
                if (_enemySpawnSO.EnemySpawnDatas[i].GetIntervalToSeconds().x < Timer && _enemySpawnSO.EnemySpawnDatas[i].GetIntervalToSeconds().y > Timer)
                {
                    _indexes.Add(i);
                }
            }
        }
    }
}
