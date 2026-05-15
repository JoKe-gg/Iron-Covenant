using System;
using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "UpgradeSO", menuName = "Scriptable Objects/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _level;
    [SerializeField] private int _price;
    [Header("Modifier data")]
    [SerializeField] private NegativeEffectData _effectData;
    [SerializeField] private LevelUPUpgradeData _levelUpgradeData;
    public int Id => _id;
    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public int Level => _level;
    public int Price => _price;
    public NegativeEffectData EffectData => _effectData;
    public LevelUPUpgradeData LevelUpgradeData => _levelUpgradeData;
    private void OnValidate()
    {
        if (_level <= 0)
        {
            _level = 1;
        }
        foreach (var item in _levelUpgradeData.StatModifierData)
        {
            item.OnValidate();
        }
    }
}
public enum StatType
{
    Defence,
    Damage,
    Health,
    MovementSPD,
    MaxHP,
    EXPBonus,
    RegenerationHP,
    Shield
}
[Serializable]
public class LevelUPUpgradeData
{
    [Header("ID")]
    [SerializeField] private StatType _statType;
    [Header("Modifier data")]
    [SerializeField] private List<StatModifierData> _statModifierData;
    public StatType StatType => _statType;
    public List<StatModifierData> StatModifierData => _statModifierData;
}
public enum StatModifierType
{
    Multiple,
    Flat
}
[Serializable]
public class StatModifierData
{
    [SerializeField] private StatModifierType _statModifierType;
    [SerializeField] private float _value;
    public StatModifierType StatModifierType => _statModifierType;
    public float Value => _value;
    public void OnValidate()
    {
        if (_value < 0)
        {
            _value = 0;
        }
        switch (_statModifierType)
        {
            case StatModifierType.Multiple:
                break;
            case StatModifierType.Flat:
                _value = Mathf.Floor(_value); 
                break;
            default:
                break;
        }
    }
}
