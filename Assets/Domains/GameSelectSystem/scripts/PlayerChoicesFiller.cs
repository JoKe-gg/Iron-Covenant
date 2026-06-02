using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class PlayerChoicesFiller : Savable
{
    [SerializeField] private GameObject _playerChoicePanelPrefab;
    [SerializeField] private PlayerChoicesSO _playerChoicesSO;
    [SerializeField] private GameObject _PlayerChoicesContent;
    [SerializeField] private UiInfoPanelSetter _uiInfoPanelSetter;
    private Dictionary<int, UnlockedPlayerChoiceDataSave> _unlockedChoices = new();
    private void ClearUI()
    {
        foreach (Transform child in _PlayerChoicesContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void Load(DataSave dataSave)
    {
        _unlockedChoices.Clear();
        foreach(var value in dataSave.UnlockedPlayerChoiceList)
        {
            _unlockedChoices.Add(value.Id, new(value.Id, value.IsPurchased));
        }
    }
    public override void Save(DataSave dataSave)
    {
        dataSave.SetUnlockedPlayerChoices(_unlockedChoices.Values.ToList());
    }
    public void CreateUI()
    {
        ClearUI();

        InstantiatePlayerChoice(_playerChoicesSO.DefaultPlayerChoiceData, true);

        foreach (var playerChoiceData in _playerChoicesSO.PlayerChoicesData)
        {
                if (_unlockedChoices.TryGetValue(playerChoiceData.PlayerId, out UnlockedPlayerChoiceDataSave unlockedPlayerChoiceDataSave))
                {
                    bool isPurchased = unlockedPlayerChoiceDataSave.IsPurchased;
                    InstantiatePlayerChoice(playerChoiceData, isPurchased, true);
                }
                else
                {
                    InstantiatePlayerChoice(playerChoiceData, false);
                }
        }
    }
    private void InstantiatePlayerChoice(PlayerChoiceData playerChoiceData, bool isPurchased, bool isUnlocked = false)
    {
        GameObject playerChoice = Instantiate(_playerChoicePanelPrefab, _PlayerChoicesContent.transform);
        playerChoice.GetComponent<PlayerChoiceSetter>().SetChoice(playerChoiceData.WeaponSprite, playerChoiceData.PlayerSprite, playerChoiceData.PlayerId, playerChoiceData.PlayerName);
        if (isPurchased)
        {
            isUnlocked = isPurchased;
        }
        playerChoice.GetComponent<PlayerChoiceBehaviour>().SetPurchasePanel(isPurchased, isUnlocked, this, playerChoiceData.Price, playerChoiceData.PlayerId);
        playerChoice.GetComponent<PlayerChoiceBehaviour>().SetInfoPanelData(
            playerChoiceData.PlayerSpriteOnGame, 
            SetDescriptionText(playerChoiceData), 
            playerChoiceData.weaponStats.WeaponType.ToString(),
            _uiInfoPanelSetter);
    }
    private List<InfoTextData> SetDescriptionText(PlayerChoiceData playerChoiceData)
    {
        List<InfoTextData> infoTextDatas = new List<InfoTextData>();
        infoTextDatas.Add(new(playerChoiceData.PlayerName, TextType.Title1, TMPro.TextAlignmentOptions.Midline));
        infoTextDatas.Add(new(SetCharacterDescription(playerChoiceData.basicStats.PlayerBasicStatsData), TextType.Paragraph, TMPro.TextAlignmentOptions.MidlineLeft));
        infoTextDatas.Add(new("Weapon", TextType.Title2, TMPro.TextAlignmentOptions.Midline));
        infoTextDatas.Add(new(SetWeaponDescriotion(playerChoiceData.weaponStats), TextType.Paragraph, TMPro.TextAlignmentOptions.MidlineLeft));
        return infoTextDatas;
    }
    private string SetCharacterDescription(PlayerBasicStatsData playerBasicStats)
    {
        string text = $"Character basic stats: \n" +
            $"Hp : {playerBasicStats.HP} \n" +
            $"Resistance : {playerBasicStats.Resistance} \n" +
            $"Movement speed : {playerBasicStats.MovementSpeed} \n";
        return text;
    }
    private string SetWeaponDescriotion(WeaponStatsSO weaponStatsSO)
    {
        string text = "Weapon basic stats: \n";
        text += $"Attack:\n";
        text += $"Damage data : {weaponStatsSO.DamageData.Amount} ({weaponStatsSO.DamageData.DamageType})\n";
        text += $"Cooldown : {weaponStatsSO.CoolDown}\n";
        switch (weaponStatsSO.WeaponType)
        {
            case WeaponType.range:
            {
                    text += $"Penetration : {weaponStatsSO.Penetration}\n";
                    text += $"Projectile speed : {weaponStatsSO.Speed}\n";
                    break;
            }
            case WeaponType.melee:
            {
                    break;
            }
            default: 
            {
                    break;
            }
        }
        text += $"\nAbility:\n";
        text += $"Damage data : {weaponStatsSO.AbilityDamageData.Amount} ({weaponStatsSO.AbilityDamageData.DamageType})\n";
        text += $"Cooldown : {weaponStatsSO.AbilityCoolDown}\n";
        switch (weaponStatsSO.WeaponTypeInt)
        {
            case (int)WeaponType.range:
                {
                    text += $"Penetration : {weaponStatsSO.AbilityPenetration}\n";
                    text += $"Projectile speed : {weaponStatsSO.AbilitySpeed}\n";
                    break;
                }
            case (int)WeaponType.melee:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
        return text;
    }
    public void PurchasePlayer(int id)
    {
        if (_unlockedChoices.TryGetValue(id, out UnlockedPlayerChoiceDataSave actualValue)) 
        {
            actualValue.IsPurchased = true;
        }
    }
}