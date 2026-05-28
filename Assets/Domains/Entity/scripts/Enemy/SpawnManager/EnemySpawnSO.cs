using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "EnemySpawnSO", menuName = "Scriptable Objects/EnemySpawnSO")]
public class EnemySpawnSO : ScriptableObject
{
    [SerializeField] private string _arena = "first";
    [SerializeField] private List<EnemySpawnData> _enemySpawnList;

    public string Arena => _arena;
    public List<EnemySpawnData> EnemySpawnDatas => _enemySpawnList;

    private void OnValidate()
    {
        EnemySpawnDatas.ForEach(data => {data.OnValidate(); });
        List<EnemySpawnData> validatingEnemySpawnList = new(_enemySpawnList);

        validatingEnemySpawnList.OrderBy(x => x.TimeInterval.x).ToList();

        _enemySpawnList = validatingEnemySpawnList;
    }
}
