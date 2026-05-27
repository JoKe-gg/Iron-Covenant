using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCalculateUpgrades : MonoBehaviour
{
    private PlayerUpgradeDataBase _upgradeData;
    private TotalUpgradeStorage _totalUpgradeStorage;
    private List<UpgradeSO> _levelUpgradeSOs;
    private PlayerLevelSystem _playerLevelSystem;
    public event Action OnUpgradeCalculationFinished;
    void Start()
    {
        _upgradeData = GetComponent<PlayerUpgradeDataBase>();
        _totalUpgradeStorage = GetComponent<TotalUpgradeStorage>();
        _playerLevelSystem = GetComponent<PlayerLevelSystem>();
        _levelUpgradeSOs = _upgradeData.GetLevelUpgradeSOList();
        if (_levelUpgradeSOs.Count > 0 )
        {
            _upgradeData.OnUpgradeListChanged += ReCalculate;
            Calculate();
        }
    }
    private void OnDisable()
    {
        if (_upgradeData != null)
        {
            _upgradeData.OnUpgradeListChanged -= ReCalculate;
        }
    }
    public void ReCalculate()
    {
        _levelUpgradeSOs = _upgradeData.GetLevelUpgradeSOList();
        Calculate();
    }
    private void Calculate()
    {
        int currentPlayerLevel = _playerLevelSystem.CurrentLevel;
        _totalUpgradeStorage.ResetStorage();

        List<UpgradeSO> levelUpgradeSOList = new(_levelUpgradeSOs);

        foreach (var upgradeSO in levelUpgradeSOList)
        {
            List<int> flatModifiers = new List<int>();
            List<float> multipleModifiers = new List<float>();

            List<StatModifierData> statModifierDatas = upgradeSO.LevelUpgradeData.StatModifierData;
            foreach (var statModifierData in statModifierDatas)
            {
                switch (statModifierData.StatModifierType)
                {
                    case StatModifierType.Flat:
                        flatModifiers.Add(Mathf.FloorToInt(statModifierData.Value));
                        break;
                    case StatModifierType.Multiple:
                        multipleModifiers.Add(statModifierData.Value);
                        break;
                    default:
                        break;
                }
            }
            int totalFlat = CalculateFlat(flatModifiers);
            float totalMultiple = CalculateMultiple(multipleModifiers);

            _totalUpgradeStorage.AddNewTotalUpgrade(upgradeSO.LevelUpgradeData.StatType, totalFlat, totalMultiple);
        }

        OnUpgradeCalculationFinished?.Invoke();
    }
    private int CalculateFlat(List<int> flatModifiers)
    {
        int totalFlat = 0;
        foreach (int modifier in flatModifiers)
        {
            totalFlat += modifier;
        }
        return totalFlat;
    }
    private float CalculateMultiple(List<float> multipleModifiers)
    {
        float totalMultiple = 1;
        foreach (float modifier in multipleModifiers)
        {
            if (modifier > 0)
            {
                totalMultiple *= modifier;
            }
        }
        return totalMultiple;
    }
}
