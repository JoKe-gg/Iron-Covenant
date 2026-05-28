using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class PlayerChoicesFiller : Savable
{
    [SerializeField] private GameObject _playerChoicePanelPrefab;
    [SerializeField] private PlayerChoicesSO _playerChoicesSO;
    [SerializeField] private GameObject _PlayerChoicesContent;
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
    }
    public void PurchasePlayer(int id)
    {
        if (_unlockedChoices.TryGetValue(id, out UnlockedPlayerChoiceDataSave actualValue)) 
        {
            actualValue.IsPurchased = true;
        }
    }
}