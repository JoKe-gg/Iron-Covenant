using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class UpgradeChoiceResult
{
    public List<UpgradeSO> Choices { get; private set;}
    public bool IsStub { get; private set; }
    public UpgradeChoiceResult(List<UpgradeSO> choices) : this(choices, false)
    {
        Choices = choices;
    }
    public UpgradeChoiceResult(List<UpgradeSO> choices, bool isStub)
    {
        Choices = choices;
        IsStub = isStub;
    }
}
public class SelectableUpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeBaseSO _originUpgrades;
    private List<UpgradeSO> _upgrades = new();
    private void Awake()
    {
        _upgrades = new(_originUpgrades.UpgradeList);
    }
    public UpgradeChoiceResult GetChoices(int requiredAmount = 2)
    {
        if (_upgrades.Count == 0)
        {
            return new UpgradeChoiceResult(_originUpgrades.StubList, true);
        }
        List<UpgradeSO> availableSelectableUpgrades = GetAvailableChoices();
        List<UpgradeSO> choices = new();

        if (availableSelectableUpgrades.Count <= requiredAmount)
        {
            return new UpgradeChoiceResult(availableSelectableUpgrades);
        }

        for (int i = 0; i < requiredAmount; i++)
        {
            int randIndex = Random.Range(0, availableSelectableUpgrades.Count);
            UpgradeSO newSelect = availableSelectableUpgrades[randIndex];
            choices.Add(newSelect);
            availableSelectableUpgrades.RemoveAt(randIndex);
        }
        return new UpgradeChoiceResult(choices);
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
