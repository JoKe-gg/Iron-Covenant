using UnityEngine;
using System.Collections.Generic;
public class SetSelects : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeSelectPrefab;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private SelectableUpgradeManager _upgradeManager;
    private void Awake()
    {
        if (_upgradeSelectPrefab == null)
        {
            Debug.LogError($"Null reference to {nameof(_upgradeSelectPrefab)} int the script {nameof(SetSelects)}");
        }
        if (_contentTransform == null)
        {
            Debug.LogError($"Null reference to {nameof(_contentTransform)} int the script {nameof(SetSelects)}");
        }
        if (_upgradeManager == null)
        {
            Debug.LogError($"Null reference to {nameof(_upgradeManager)} int the script {nameof(SetSelects)}");
        }
    }
    private void OnEnable()
    {
        List<UpgradeSO> list = _upgradeManager.GetChoices();
        InitializeSelects(list);
    }
    public void CloseSelectPanel()
    {
        gameObject.SetActive(false);
        foreach (Transform child in _contentTransform)
        {
            Destroy(child.gameObject);
        }
        PauseManager.instance.SetPause(false, false);
    }
    public void InitializeSelects(List<UpgradeSO> UpgradeSOList)
    {
        if (UpgradeSOList == null || UpgradeSOList.Count == 0)
        {
            CloseSelectPanel();
        }
        foreach (var upgradeSO in UpgradeSOList)
        {
            GameObject newSelect = Instantiate(_upgradeSelectPrefab, _contentTransform);
            newSelect.GetComponent<ButtonActionForChoice>().SetSelectableUpgrade(upgradeSO, this, _upgradeManager);
            newSelect.GetComponent<UpgradeChoiceBehaviour>().Initialize(upgradeSO);
        }
    }
}
