 using System.Runtime.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerLevelSystem))]
[RequireComponent(typeof(PlayerUpgradeDataBase))]
[RequireComponent(typeof(PlayerCalculateUpgrades))]
[RequireComponent(typeof(TotalUpgradeStorage))]
[RequireComponent(typeof(SortingLayerUpdate))]
[RequireComponent(typeof(PlayerStatuses))]

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBasicStatsSO _playerBasicStatsSO;
    private PlayerLevelSystem _playerLevelSystem;
    public PlayerBasicStatsSO PlayerBasicStatsSO => _playerBasicStatsSO;
    private void Awake()
    {
        bool error = false;
        _playerLevelSystem = GetComponent<PlayerLevelSystem>();
        if (_playerBasicStatsSO == null)
        {
            Debug.LogError($"Null Reference to {nameof(PlayerBasicStatsSO)} in the script {nameof(PlayerBehaviour)}");
            error = true;
        }
        if (error) 
        {
            return;   
        }
    }
}