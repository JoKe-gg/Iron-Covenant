using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;
public class ConstUpgradeUIData
{
    public int StartLevel { get; private set; }
    public List<UpgradeSO> UpgradeSOs { get; private set; } = new();
    public ConstUpgradeUIData(UpgradeSO upgradeSO, int currentLevel)
    {
        AddNewLevel(upgradeSO);
        StartLevel = currentLevel;
    }
    public void AddNewLevel(UpgradeSO upgradeSO)
    {
        UpgradeSOs.Add(upgradeSO);
        UpgradeSOs = UpgradeSOs.OrderBy(u => u.Level).ToList();
    }
}
public class ConstUpgradeManager : Savable
{
    public static ConstUpgradeManager instance { get; private set; }
    [SerializeField] private UpgradeBaseSO _constUpgradeBase;
    public List<ConstUpgradeUIData> ConstUpgradeListForUI => GetConstUpgradeListUI();
    public Dictionary<int, UpgradeSO> ConstUpgradeDictionary { get; private set; } = new();
    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override void Load(DataSave dataSave)
    {
        ConstUpgradeDictionary.Clear();
        foreach (var item in dataSave.ConstUpgradeList)
        {
            foreach (var upgradeSO in _constUpgradeBase.UpgradeList)
            {
                if (item.Id == upgradeSO.Id && item.Level == upgradeSO.Level)
                {
                    ConstUpgradeDictionary[item.Id] = upgradeSO;
                    break;
                }
            }
        }
    }
    private List<ConstUpgradeUIData> GetConstUpgradeListUI()
    {
        Dictionary<int, ConstUpgradeUIData> NewConstUpgradeDictionary = new();
        foreach (var constBasicUpgrade in _constUpgradeBase.UpgradeList)
        {
            if (NewConstUpgradeDictionary.TryGetValue(constBasicUpgrade.Id, out ConstUpgradeUIData constUpgradeUIData))
            {
                constUpgradeUIData.AddNewLevel(constBasicUpgrade);
            }
            else
            {
                int startLevel = ConstUpgradeDictionary.TryGetValue(constBasicUpgrade.Id, out UpgradeSO upgradeSO)
                    ? upgradeSO.Level
                    : 0;
                NewConstUpgradeDictionary[constBasicUpgrade.Id] = new(constBasicUpgrade, startLevel);
            }
        }
        if (NewConstUpgradeDictionary .Count == 0)
        {
            return new List<ConstUpgradeUIData>();
        }
        return NewConstUpgradeDictionary.Values.ToList();
    }
    public override void Save(DataSave dataSave)
    {
        dataSave.ConstUpgradeList.Clear();

        foreach (var item in ConstUpgradeDictionary)
        {
            ConstUpgradeDataSave constUpgradeDataSave = new(item.Value);
            dataSave.ConstUpgradeList.Add(constUpgradeDataSave);
        }
    }
    public LevelUPUpgradeData GetPermanentStatModifier(StatType statType)
    {
        foreach (var item in ConstUpgradeDictionary.Values)
        {
            if (item.LevelUpgradeData.StatType == statType)
            {
                return item.LevelUpgradeData;
            }
        }
        return null;
    }
    public NegativeEffectData GetPermanentEffect(StatusEffectType statusEffectType)
    {
        foreach (var item in ConstUpgradeDictionary.Values)
        {
            if (item.EffectData.EffectType == statusEffectType)
            {
                return item.EffectData;
            }
        }
        return null;
    }
    public void AddConstUpgrade(UpgradeSO upgradeSO)
    {
        if (upgradeSO == null)
        {
            Debug.LogError("Permanent UpgradeSO is null");
            return;
        }
        if (ConstUpgradeDictionary.TryGetValue(upgradeSO.Id, out UpgradeSO currentUpgrade))
        {
            if (currentUpgrade.Level < upgradeSO.Level)
            {
                ConstUpgradeDictionary[upgradeSO.Id] = upgradeSO;
            }
        }
        else
        {
            ConstUpgradeDictionary[upgradeSO.Id] = upgradeSO;
        }    
    }
}