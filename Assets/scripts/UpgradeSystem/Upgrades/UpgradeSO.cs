using System;
using UnityEngine;
using System.Collections.Generic;
public enum TypeOfAddedUpgrade 
{ 
    statsModifier,
    newEffect
}
[CreateAssetMenu(fileName = "UpgradeSO", menuName = "Scriptable Objects/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int _id;
    [SerializeField] private List<TypeOfAddedUpgrade> _upgradeTypes;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _level;
    [SerializeField] private int _price;
    [Header("Modifier data")]
    [SerializeField] private NegativeEffectData _effectData;
    [SerializeField] private LevelUPUpgradeData _levelUpgradeData;
    [SerializeField] private StubData _stubData;
    public int Id => _id;
    public List<TypeOfAddedUpgrade > UpgradeTypes => _upgradeTypes;
    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public int Level => _level;
    public int Price => _price;
    public NegativeEffectData EffectData => _effectData;
    public LevelUPUpgradeData LevelUpgradeData => _levelUpgradeData;
    public StubData StubData => _stubData;
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
        if (_effectData != null)
        {
            _effectData.SetLevel(_level);
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
    Shield,
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
public enum StubType
{
    coins,
    regenerationHP
}
[Serializable]
public class StubData
{
    [SerializeField] private StubType _stubType;
    [SerializeField, Min(1)] private int _value;

    public StubType StubType => _stubType;
    public int Value => _value;
}