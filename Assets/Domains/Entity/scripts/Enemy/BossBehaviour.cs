using UnityEngine;
using System.Collections.Generic;
using System;
[RequireComponent(typeof(Enemy))]
public class BossBehaviour : Savable
{
    private Enemy _enemy;
    private bool _toDie = false;
    protected override void Awake()
    {
        base.Awake();
        _enemy = GetComponent<Enemy>();
    }
    public void OnBossDied()
    {
        StateManager.instance.SetState(RuntimeState.Victory);
        _toDie = true;
        SaveManager.SaveGame();
    }
    public override void Load(DataSave saveData)
    {
        return;
    }
    public override void Save(DataSave saveData)
    {
        if (_toDie)
        {
            bool isCharacterUnlocked = false;
            bool isMapUnlocked = false;
            foreach (var character in saveData.UnlockedPlayerChoiceList)
            {
                if (character.Id == _enemy.BasicStatsEnemySO.basicStats.Data.PlayerId)
                {
                    isCharacterUnlocked = true;
                    break;
                }
            }
            foreach (var map in saveData.UnlockedMapChoiceList)
            {
                if (map.Id == _enemy.BasicStatsEnemySO.basicStats.Data.MapId)
                {
                    isMapUnlocked = true;
                    break;
                }
            }
            foreach (var type in _enemy.BasicStatsEnemySO.basicStats.Data.Regards)
            {
                switch (type)
                {
                    case BossRegardType.NewMap:
                        if (!isMapUnlocked)
                        {
                            List<UnlockedMapChoiceData> mapIdSaveData = new(saveData.UnlockedMapChoiceList);
                            UnlockedMapChoiceData unlockedMapChoiceData = new UnlockedMapChoiceData(_enemy.BasicStatsEnemySO.basicStats.Data.MapId);
                            mapIdSaveData.Add(unlockedMapChoiceData); 
                            saveData.SetUnlockedMapChoices(mapIdSaveData);
                        }
                        break;
                    case BossRegardType.NewPlayer:
                        if (!isCharacterUnlocked)
                        {
                            List<UnlockedPlayerChoiceDataSave> playersIdSaveData = new(saveData.UnlockedPlayerChoiceList);
                            UnlockedPlayerChoiceDataSave playerIdSaveData = new UnlockedPlayerChoiceDataSave(_enemy.BasicStatsEnemySO.basicStats.Data.PlayerId);
                            playersIdSaveData.Add(playerIdSaveData);
                            saveData.SetUnlockedPlayerChoices(playersIdSaveData);
                        }
                        break;
                    default:
                        break;
                }
            }
            _enemy.ReturnToPool();
        }
    }
}
