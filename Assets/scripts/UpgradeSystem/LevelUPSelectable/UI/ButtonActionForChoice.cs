using Unity.Multiplayer.PlayMode;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonActionForChoice : MonoBehaviour
{
    private UpgradeSO _upgradeSO;
    private SetSelects _setSelects;
    private SelectableUpgradeManager _selectableUpgradeManager;
    public void AddSelectable()
    {
        if (_upgradeSO == null)
        {
            Debug.LogError($"Null reference to {nameof(_upgradeSO)} in the script {nameof(ButtonActionForChoice)}");
            return;
        }
        PlayerStatuses playerStatuses = PlayerSpawnManager.CurrentPlayer.GetComponent<PlayerStatuses>();
        if (_upgradeSO.LevelUpgradeData != null)
            playerStatuses.AddNewUpgrade(_upgradeSO);
        if (_upgradeSO.EffectData != null) { }
            playerStatuses.AddNewEffect(_upgradeSO.EffectData);
        _setSelects.CloseSelectPanel();
        _selectableUpgradeManager.RemoveChoice(_upgradeSO);
    }
    public void SetSelectableUpgrade(UpgradeSO upgradeSO, SetSelects setSelect, SelectableUpgradeManager selectableUpgradeManager)
    {
        _setSelects = setSelect;
        _upgradeSO = upgradeSO;
        _selectableUpgradeManager = selectableUpgradeManager;
    }
}
