using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlayerUpgradeDataBase : MonoBehaviour
{
    private List<UpgradeSO> _upgradeSOList = new();
    public event Action<List<UpgradeSO>> OnUpgradeListChanged;
    public void Awake()
    {
        _upgradeSOList = new();
    }
    public List<UpgradeSO> GetLevelUpgradeSOList()
    {
        return _upgradeSOList;
    }
    public void AddNewUpgrade(UpgradeSO upgradeSO)
    {
        _upgradeSOList.Add(upgradeSO);
        OnUpgradeListChanged?.Invoke(_upgradeSOList);
    }

}
