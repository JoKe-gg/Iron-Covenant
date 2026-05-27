using System.Collections.Generic;
using UnityEngine;

public class InitializeEndMatchStats : MonoBehaviour
{
    [SerializeField] private InitializableEnemyKilledAmount _prefab;
    [SerializeField] private Transform _content;
    private void OnEnable()
    {
        foreach (Transform child in _content)
        {
            Destroy(child);
        }
        InitializeStats();
    }
    public void InitializeStats()
    {
        Dictionary<string, int> stats = RuntimeStats.Instance.KilledEnemiesDictionary;

        foreach(var item in stats)
        {
            InitializableEnemyKilledAmount newStatPanel = Instantiate(_prefab, _content);
            newStatPanel.Initialize(item.Key, item.Value);
        }
    }
}
