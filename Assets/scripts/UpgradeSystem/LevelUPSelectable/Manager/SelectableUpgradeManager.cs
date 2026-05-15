using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class SelectableUpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeBaseSO _originUpgrades;
    private List<UpgradeSO> _upgrades;
    private void Awake()
    {
        _upgrades = new(_originUpgrades.UpgradeList);
    }
    public List<UpgradeSO> GetChoices(int requiredAmount = 2)
    {
        if (_upgrades == null || _upgrades.Count == 0)
        {
            return new List<UpgradeSO>();
        }
        List<UpgradeSO> availableSelectableUpgrades = GetAvailableChoices();
        List<UpgradeSO> choices = new();

        if (availableSelectableUpgrades.Count <= requiredAmount)
        {
            return availableSelectableUpgrades;
        }

        for (int i = 0; i < requiredAmount; i++)
        {
            int randIndex = Random.Range(0, availableSelectableUpgrades.Count);
            UpgradeSO newSelect = availableSelectableUpgrades[randIndex];
            choices.Add(newSelect);
            availableSelectableUpgrades.RemoveAt(randIndex);
        }
        return choices;
    }
    private List<UpgradeSO> GetAvailableChoices()
    {
        Dictionary<int, UpgradeSO> availableChoices = new();
        foreach (var upgrade in _upgrades)
        {
            if (availableChoices.TryGetValue(upgrade.Id, out UpgradeSO value)) 
            {
                if (upgrade.Level < value.Level)
                {
                    availableChoices[upgrade.Id] = upgrade;
                }
            }
            else
            {
                availableChoices[upgrade.Id] = upgrade;
            }
        }
        List<UpgradeSO> selectableUpgrades = availableChoices.Values.ToList();
        return selectableUpgrades;
    }
    public void RemoveChoice(UpgradeSO upgradeSO)
    {
        _upgrades.RemoveAll(u => u == upgradeSO);
    }
}
