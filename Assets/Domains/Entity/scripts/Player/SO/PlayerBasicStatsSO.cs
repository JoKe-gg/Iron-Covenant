using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBasicStatsSO", menuName = "Scriptable Objects/PlayerBasicStatsSO")]
public class PlayerBasicStatsSO : ScriptableObject
{
    [Header("Basic stats id")]
    [SerializeField] private int _id = 0;
    [SerializeField] private string _playerName = "gunner";
    [Header("Basic stats data")]
    [SerializeField] private PlayerBasicStatsData _playerBasicStatsData;
    
    public int Id => _id;
    public string PlayerName => _playerName;
    public PlayerBasicStatsData PlayerBasicStatsData => _playerBasicStatsData;
}
