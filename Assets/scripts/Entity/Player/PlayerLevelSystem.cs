using System;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour, IChangingBar
{
    [SerializeField] private int _exp = 400;
    [SerializeField] private BarController _expBarController;
    private PlayerCalculateUpgrades _playerCalculateUpgrades;
    private PlayerBehaviour _playerBehaviour;
    public int CurrentLevel { get; private set; }
    private int _xpToNextLevel;
    private int _basicXpToNextLevel;
    public event Action<int> OnLevelUP;
    private void Start()
    {
        if (_expBarController == null)
        {
            _expBarController = GameObject.FindGameObjectWithTag("PlayerEXPBar").GetComponent<BarController>();
        }
        CurrentLevel = 0;
        _playerCalculateUpgrades = GetComponent<PlayerCalculateUpgrades>();
        _playerBehaviour = GetComponent<PlayerBehaviour>();
        _basicXpToNextLevel = _playerBehaviour.PlayerBasicStatsSO.PlayerBasicStatsData.XP;
        PurchaseEXP(0);
    }
    public void PurchaseEXP(int value)
    {
        value = Mathf.Max(value, 0);
        _exp += value;

        RuntimeStats.Instance.AddXP(value);
        UpdateXpTpNextLevel();

        if (_exp >= _xpToNextLevel)
        {
            LevelUP();
        }
    }
    public void ChangeBar() => _expBarController.ChangeBar(_exp, _xpToNextLevel);

    private void UpdateXpTpNextLevel()
    {
        _xpToNextLevel = Mathf.RoundToInt(_basicXpToNextLevel * Mathf.Pow(1.1f, CurrentLevel));
        ChangeBar();
    }
    
    private void LevelUP()
    {
        while (_exp >= _xpToNextLevel) {
            CurrentLevel++;

            _exp -= _xpToNextLevel;
            UpdateXpTpNextLevel();
            Debug.Log($"Exp is {_exp}/{_xpToNextLevel}.\n Current level: {CurrentLevel}");
        }
        ChangeBar();
        OnLevelUP?.Invoke(CurrentLevel);
        _playerCalculateUpgrades.ReCalculate();
    }
}
