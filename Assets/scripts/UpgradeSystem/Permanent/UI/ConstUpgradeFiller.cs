using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class ConstUpgradeFiller : MonoBehaviour
{
    [Header("Filler options")]
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _constPrefab;
    private List<ConstUpgradeUIData> _constUpgradeBase;
    private Dictionary<int, ConstUpgradeBehaviour> _constUpgradeDictionary = new();

    private void OnEnable()
    {
        _constUpgradeBase = ConstUpgradeManager.instance.ConstUpgradeListForUI;
        Debug.Log($"Const upgrade base amount = {_constUpgradeBase.Count}");
        ClearUI();
        InitializeConstantUpgrades();
    }
    private void ClearUI()
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }

        _constUpgradeDictionary.Clear();
    }
    private void InitializeConstantUpgrades()
    {
        foreach (var constUpgrade in _constUpgradeBase)
        {
            List<UpgradeSO> constUpgradeSOs = constUpgrade.UpgradeSOs;
            if (constUpgradeSOs != null)
            {
                GameObject upgrade = Instantiate(_constPrefab, _content);
                ConstUpgradeBehaviour constUpgradeBehaviour = upgrade.GetComponent<ConstUpgradeBehaviour>();
                constUpgradeBehaviour.Initialize(constUpgradeSOs, constUpgrade.StartLevel);
                _constUpgradeDictionary.Add(constUpgradeSOs[0].Id, constUpgradeBehaviour);
            }
        }
    }
}
