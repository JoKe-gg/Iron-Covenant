using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerUpgradeDataBase : MonoBehaviour
{
    [SerializeField] private UpgradeBaseSO _levelUpgradeBaseOrigin;
    private List<UpgradeSO> _upgradeSOList;
    public event Action OnUpgradeListChanged;
    public void Awake()
    {
        _upgradeSOList = new(_levelUpgradeBaseOrigin.UpgradeList);
    }
    public List<UpgradeSO> GetLevelUpgradeSOList()
    {
        return _upgradeSOList;
    }
    public void AddNewUpgrade(UpgradeSO upgradeSO)
    {
        _upgradeSOList.Add(upgradeSO);
        OnUpgradeListChanged?.Invoke();
    }
}
