using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

[Serializable]
public class OnPauseStatDisplay
{
    [SerializeField] private TextMeshProUGUI _statTypeText;
    [SerializeField] private TextMeshProUGUI _statModifierText;
    [SerializeField] private StatModifierType _statModifierType;

    public TextMeshProUGUI StatTypeText => _statTypeText;
    public TextMeshProUGUI StatModifierText => _statModifierText;
    public StatModifierType StatModifierType => _statModifierType;
}
public class StatOnPauseSetter : MonoBehaviour
{
    [SerializeField] private Color _color = Color.azure;
    [SerializeField] private List<OnPauseStatDisplay> _onPauseStatDisplayList = new();
    // D4C5C5
    private void Awake()
    {
        bool hasMultipleText = false;
        bool hasFlatText = false;
        foreach (var item in _onPauseStatDisplayList)
        {
            switch (item.StatModifierType)
            {
                case StatModifierType.Flat: 
                    hasFlatText = true;
                    break;
                case StatModifierType.Multiple:
                    hasMultipleText = true;
                    break;
                default:
                    break;
            }
        }
        if (!(hasMultipleText && hasFlatText))
        {
            Destroy(gameObject);
        }
    }
    public void Initialize(StatType stat, int flatModifier, float multipleModifier)
    {
        if (!(Math.Round(multipleModifier - 1f, 2) * 100 == 0 && flatModifier == 0))
        {
            GetComponent<UnityEngine.UI.Image>().color = _color;
        }
        foreach (var item in _onPauseStatDisplayList)
        {
            item.StatTypeText.text = stat.ToString().ToLower();
            switch (item.StatModifierType)
            {
                case StatModifierType.Flat:
                    item.StatModifierText.text = $"+ {flatModifier}";
                    break;
                case StatModifierType.Multiple:
                    item.StatModifierText.text =$"+ {Math.Round(multipleModifier - 1f, 2) * 100}";
                    break;
                default:
                    break;
            }
        }
    }
}
