using UnityEngine;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(fileName = "UpgradeBaseSO", menuName = "Scriptable Objects/UpgradeBaseSO")]
public class UpgradeBaseSO : ScriptableObject
{
    [SerializeField] private string _name = "Upgrade data base";
    [SerializeField] private List<UpgradeSO> _upgradeList = new();
    public string Name => _name;
    public List<UpgradeSO> UpgradeList => _upgradeList;
    private void OnValidate()
    {
        _upgradeList = _upgradeList.OrderBy(u => u.Id).ToList();
    }
}
