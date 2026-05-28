using Unity.Multiplayer.PlayMode;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonActionForChoice : MonoBehaviour
{
    private UpgradeSO _upgradeSO;
    private SetSelects _setSelects;
    private SelectableUpgradeManager _selectableUpgradeManager;
    bool _isStub = false;
    public void AddSelectable()
    {
        if (_upgradeSO == null)
        {
            Debug.LogError($"Null reference to {nameof(_upgradeSO)} in the script {nameof(ButtonActionForChoice)}");
            return;
        }
        PlayerStatuses playerStatuses = PlayerSpawnManager.CurrentPlayer.GetComponent<PlayerStatuses>();
        if (_isStub)
        {
            if (_upgradeSO.StubData != null)
            {
                switch (_upgradeSO.StubData.StubType)
                {
                    case StubType.regenerationHP:
                        int regenValue = Mathf.Max(_upgradeSO.StubData.Value, 0);
                        playerStatuses.RestoreHP(regenValue);
                        break;
                    case StubType.coins:
                        int coinsValue = Mathf.Max(_upgradeSO.StubData.Value, 0);
                        CoinsManager.instance.AddCoins(coinsValue);
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            foreach (var type in _upgradeSO.UpgradeTypes)
            {
                switch (type)
                {
                    case TypeOfAddedUpgrade.newEffect:
                        if (_upgradeSO.EffectData != null)
                        {
                            playerStatuses.AddNewEffect(_upgradeSO.EffectData);
                        }
                        break;
                    case TypeOfAddedUpgrade.statsModifier:
                        if (_upgradeSO.LevelUpgradeData != null)
                        {
                            playerStatuses.AddNewUpgrade(_upgradeSO);
                        }
                        break;
                    default:
                        break;
                }
                _selectableUpgradeManager.RemoveChoice(_upgradeSO);
            }
        }
        _setSelects.CloseSelectPanel();
    }
    public void SetSelectableUpgrade(UpgradeSO upgradeSO, SetSelects setSelect, SelectableUpgradeManager selectableUpgradeManager, bool isStub = false)
    {
        _setSelects = setSelect;
        _upgradeSO = upgradeSO;
        _selectableUpgradeManager = selectableUpgradeManager;
        _isStub=isStub;
    }
}
