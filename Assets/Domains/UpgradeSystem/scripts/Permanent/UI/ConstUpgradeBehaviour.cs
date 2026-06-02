using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ButtonHoverEffect))]
public class ConstUpgradeBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _constUpgradeSprite;
    private Button _constUpgradeButton;
    Dictionary<int, UpgradeSO> _upgradeSODictionary = new();
    public int Level { get; private set; } = 1;
    private int _price;

    private Sprite _playerSprite;
    private List<InfoTextData> _infoTextDatas;
    private string _titleText;
    private UiInfoPanelSetter _uiInfoPanelSetter;

    private void Awake()
    {
        _constUpgradeButton = GetComponent<Button>();
    }
    public void Initialize(List<UpgradeSO> upgradeSOs, int startLevel, UiInfoPanelSetter uiInfoPanelSetter)
    {
        _uiInfoPanelSetter = uiInfoPanelSetter;
        foreach (var upgradeSO in upgradeSOs)
        {
            _upgradeSODictionary[upgradeSO.Level] = upgradeSO;
        }
        EndInitialize(startLevel);
    }
    public void EndInitialize(int startLevel)
    {
        Level = startLevel + 1;
        SetCurrentLevel();
    }
    private void SetCurrentLevel()
    {
        if (!_upgradeSODictionary.TryGetValue(Level, out UpgradeSO upgradeSO)) 
        { 
            Destroy(gameObject);
            return;
        }
        else
        {
            _nameText.text = upgradeSO.Name;
            _price = upgradeSO.Price;
            _priceText.text = _price.ToString();
            _constUpgradeSprite.sprite = upgradeSO.Sprite;
            if (CoinsManagerMainMenu.instance != null)
            {
                SetEnable(CoinsManagerMainMenu.instance.Coins);
            }
        }
            
    }
    private void OnEnable()
    {
        if (CoinsManagerMainMenu.instance != null)
            CoinsManagerMainMenu.instance.OnCoinsChanged += SetEnable;
    }
    private void OnDisable()
    {
        if (CoinsManagerMainMenu.instance != null)
            CoinsManagerMainMenu.instance.OnCoinsChanged -= SetEnable;
    }
    private void SetEnable(int amount)
    {
        bool interactable = amount >= _price;
        _constUpgradeButton.interactable = interactable;
        _constUpgradeSprite.color = interactable ? Color.white : Color.darkGray;
    }
    public void AddUpgrade()
    {
        Debug.Log("Pressed this");
        if (ConstUpgradeManager.instance == null)
        {
            return;
        }
        if (CoinsManagerMainMenu.instance == null) 
        {
            return;
        }
        if (_upgradeSODictionary == null) 
        {
            return;
        }
        UpgradeSO constUpgrade = _upgradeSODictionary[Level];
        CoinsManagerMainMenu.instance.SpendCoins(_price);
        ConstUpgradeManager.instance.AddConstUpgrade(constUpgrade);
        Level++;
        SetCurrentLevel();
    }

    public void OnHoverInfoSet()
    {
        _uiInfoPanelSetter.Initialize(SetDescriptionText(_upgradeSODictionary[Level]), _upgradeSODictionary[Level].Name, _upgradeSODictionary[Level].Sprite);
    }
    private List<InfoTextData> SetDescriptionText(UpgradeSO upgradeSo)
    {
        List<InfoTextData> infoTextDatas = new List<InfoTextData>();
        foreach (TypeOfAddedUpgrade typeOfAddedUpgrade in upgradeSo.UpgradeTypes)
        {
            switch (typeOfAddedUpgrade)
            {
                case TypeOfAddedUpgrade.newEffect:
                    {
                        infoTextDatas.Add(new("Effect", TextType.Title1, TMPro.TextAlignmentOptions.Midline));
                        infoTextDatas.Add(new(SetEffectDescription(upgradeSo.EffectData), TextType.Paragraph, TMPro.TextAlignmentOptions.MidlineLeft));
                        break;
                    }
                case TypeOfAddedUpgrade.statsModifier:
                    {
                        infoTextDatas.Add(new("Stat modifier", TextType.Title1, TMPro.TextAlignmentOptions.Midline));
                        infoTextDatas.Add(new(SetStatDescription(upgradeSo.LevelUpgradeData), TextType.Paragraph, TMPro.TextAlignmentOptions.MidlineLeft));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        return infoTextDatas;
    }
    private string SetStatDescription(LevelUPUpgradeData levelUpgradeData)
    {
        string text = $"Stat modifiers ({levelUpgradeData.StatType}) :\n";
        int totalFlat = 0;
        float TotalMultiple = 1;
        foreach (StatModifierData statModifierData in levelUpgradeData.StatModifierData)
        {
            switch (statModifierData.StatModifierType)
            {
                case StatModifierType.Flat:
                    {
                        totalFlat += Mathf.FloorToInt(statModifierData.Value);
                        break;
                    }
                case StatModifierType.Multiple:
                    {
                        TotalMultiple *= statModifierData.Value;
                        break;
                    }
            }
        }
        if (totalFlat > 0)
        {
            text += $"Flat: {totalFlat}";
        }
        if (TotalMultiple > 1)
        {
            text += $"Multiple: {totalFlat}";
        }
        return text ;
    }
    private string SetEffectDescription(NegativeEffectData effectData)
    {
        string text = $"Effect modifiers :\n ";

        switch (effectData.EffectType)
        {
            case StatusEffectType.Poison:
                {
                    text += $"Deals {effectData.DamageData.Amount} of {effectData.DamageData.DamageType} Damage each {effectData.IntervalBetweenTicks} for {effectData.TimeOfEffect}";
                    break;
                }
            case StatusEffectType.Burn:
                {
                    text += $"Deals {effectData.DamageData.Amount} of {effectData.DamageData.DamageType} Damage each {effectData.IntervalBetweenTicks} for {effectData.TimeOfEffect}";
                    break;
                }
            default:
                {
                    break;
                }
        }
        return text;
    }
}
