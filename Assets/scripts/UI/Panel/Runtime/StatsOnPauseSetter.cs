using System;
using Unity.Multiplayer.PlayMode;
using UnityEngine;

public class StatsOnPauseSetter : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private StatOnPauseSetter _prefab;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        foreach(Transform item in _content)
        {
            Destroy(item.gameObject);
        }
        TotalUpgradeStorage totalUpgradeStorage = PlayerSpawnManager.CurrentPlayer.GetComponent<TotalUpgradeStorage>();
        foreach (StatType item in Enum.GetValues(typeof(StatType)))
        {
            TotalUpgrade totalUpgrade = totalUpgradeStorage.GetTotalUpgrade(item);
            Instantiate(_prefab, _content).Initialize(item, totalUpgrade.FlatModifierTotal, totalUpgrade.MultipleModifierTotal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
