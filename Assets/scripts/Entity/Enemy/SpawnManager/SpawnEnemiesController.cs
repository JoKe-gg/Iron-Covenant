using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;


public class SpawnEnemiesController : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnSO> _enemySpawnSOList;
    [SerializeField] private Tilemap _arenaTileMap;
    //Target to spawn around
    private GameObject _player;
    [Header("Spawner Settings")] 
    [SerializeField] private float _minSpawnRadius = 14f;
    [SerializeField] private float _maxSpawnRadius = 18f;
    [SerializeField] private int _limit = 100;
    [SerializeField] private float intervalBetweenSpawns = 2f;
    private bool _isAbleToSpawn = true;
    //
    private float _timer = 0f;
    //Global enemy pool
    private EnemyPool _enemyPool;

    private void Start()
    {
        _enemyPool = GameManager.instance.EnemyPool;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        StateManager.instance.OnStateChanged += OnStateChanged;
    }
    private Vector2 GetSpawnPosition()
    {
        if (_player == null) return Vector2.zero;

        const int maxAttempts = 50;

        for (int i = 0; i < maxAttempts; ++i)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(_minSpawnRadius, _maxSpawnRadius);
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 spawnPosition = (Vector2)_player.transform.position + direction * distance;
            Vector3Int cellPos = _arenaTileMap.WorldToCell(spawnPosition);
            BoundsInt bounds = _arenaTileMap.cellBounds;
            if (!_arenaTileMap.HasTile(cellPos))
            {
                return spawnPosition;
            }
        }
        return _player.transform.position;
    }
    public int GetAmountOfExistedEnemies()
    {
        int totalAmountOfEnemy;

        totalAmountOfEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

        return _limit - totalAmountOfEnemy;
    }
    private void FixedUpdate()
    {
        if (GameTimer.Instance.ElapsedTime - _timer >= intervalBetweenSpawns && _isAbleToSpawn)
        {
            _timer = GameTimer.Instance.ElapsedTime;
            SpawnEnemy();
        }
    }
    private void OnStateChanged(RuntimeState state)
    {
        switch (state)
        {
            case RuntimeState.Victory: 
                SetAbleSpawner(false);
                break;
            case RuntimeState.GameOver:
                SetAbleSpawner(false);
                break;
            default: 
                break;
        }
    }
    private void SetAbleSpawner(bool value)
    {
        _isAbleToSpawn = value;
    }
    private void SpawnEnemy()
    {
        if (_player == null)
        {
            return;
        }
        int randomAmountLimit = GetAmountOfExistedEnemies() / 8;
        if (randomAmountLimit <= 0)
        {
            return;
        }
        int randomAmountOfEnemy = Random.Range(0, randomAmountLimit + 1);
        for (int i = 0; i < randomAmountLimit; i++) 
        {
            Enemy enemy = _enemyPool.GetEnemy(out ObjectPool<Enemy> pool);
            if (enemy != null)
            {
                enemy.Initialize(pool, GetSpawnPosition());
            }
        }
    }
}
