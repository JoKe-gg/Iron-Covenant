using System.Linq.Expressions;
using UnityEngine;

public class PlayerStatuses : MonoBehaviour
{
    private TotalUpgradeStorage _totalUpgradeStorage;
    private PlayerUpgradeDataBase _upgradeDataBase;
    private void Awake()
    {
        _totalUpgradeStorage = GetComponent<TotalUpgradeStorage>();
        if (_totalUpgradeStorage == null )
        {
            Destroy(this);
            Debug.LogError($"Null reference to {nameof(_totalUpgradeStorage)} in the script {nameof(PlayerStatuses)}");
            return;
        }
        _upgradeDataBase = GetComponent<PlayerUpgradeDataBase>();
    }
    public void AddNewEffect(NegativeEffectData negativeEffectData) => _totalUpgradeStorage.AddNewEffect(negativeEffectData);
    public void AddNewUpgrade(UpgradeSO levelUpgradeSO) => _upgradeDataBase.AddNewUpgrade(levelUpgradeSO);
}
