using System;
using System.Collections.Generic;
using UnityEngine;

public class MapChoicesFiller : Savable
{
    [SerializeField] private GameObject _mapChoicePanelPrefab;
    [SerializeField] private MapChoicesSO _mapChoicesSO;
    [SerializeField] private GameObject _mapChoicesContent;

    private List<UnlockedMapChoiceData> _unlockedChoices = new();
    private void ClearUI()
    {
        foreach (Transform child in _mapChoicesContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SaveManager.LoadGame();
        CreateUI();
    }
    public override void Load(DataSave dataSave)
    {
        _unlockedChoices = dataSave.UnlockedMapChoiceList;
    }
    public override void Save(DataSave dataSave)
    {
        return;
    }
    private void CreateUI()
    {
        ClearUI();
        List<int> unlockedId = new List<int>();
        foreach (var mapChoice in _unlockedChoices)
        {
            unlockedId.Add(mapChoice.Id);
        }

        InstantiateMapChoice(_mapChoicesSO.DefaultChoice);

        foreach (var playerChoiceData in _mapChoicesSO.MapChoicesData)
        {
            foreach (int id in unlockedId)
            {
                if (playerChoiceData.MapId == id)
                {
                    InstantiateMapChoice(playerChoiceData);
                }
            }
        }
    }
    private void InstantiateMapChoice(MapChoiceData mapChoice)
    {
        GameObject playerChoice = Instantiate(_mapChoicePanelPrefab, _mapChoicesContent.transform);
        playerChoice.GetComponent<MapChoiceSetter>().SetChoice(mapChoice.MapSprite, mapChoice.MapId, mapChoice.MapName);
    }
}
