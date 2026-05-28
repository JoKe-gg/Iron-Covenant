using UnityEngine;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(fileName = "UpgradeBaseSO", menuName = "Scriptable Objects/UpgradeBaseSO")]
public class UpgradeBaseSO : ScriptableObject
{
    [SerializeField] private string _name = "Upgrade data base";
    [SerializeField] private List<UpgradeSO> _upgradeList = new();
    [SerializeField] private List<UpgradeSO> _stubList = new();


    public string Name => _name;
    public List<UpgradeSO> UpgradeList => _upgradeList;
    public List<UpgradeSO> StubList => _stubList;
    private void OnValidate()
    {
        _upgradeList = _upgradeList.OrderBy(u => u.Id).ToList();
        if (_stubList.Count > 2){
        List<UpgradeSO> newList = new List<UpgradeSO>();
            for (int i = 0; i < 2; i++)
            {
                newList.Add(_stubList[i]);
            }
            _stubList = newList;
        }
    }
}
