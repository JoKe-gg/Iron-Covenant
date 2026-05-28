using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

[Serializable]
public class DataSave
{
    public int Coins;
    public List<ConstUpgradeDataSave> ConstUpgradeList = new();
    public List<UnlockedPlayerChoiceDataSave> UnlockedPlayerChoiceList = new();
    public List<UnlockedMapChoiceData> UnlockedMapChoiceList = new();
    public List<AudioMixerGroupSaveData> AudioMixerGroupList = new();


    public void SetCoins(int coins)
    {
        Coins = coins;
    }
    public void SetConstUpgradeList(List<ConstUpgradeDataSave> constUpgradeDataSaves)
    {
        foreach (var constUpgrade in constUpgradeDataSaves)
        {
            if (constUpgrade != null)
            {
                bool doesExist = false;
                bool toReplace = false;
                ConstUpgradeDataSave constUpgradeDataSaveToRemove = null;
                foreach (var item in ConstUpgradeList)
                {
                    if (item.Id == constUpgrade.Id)
                    {
                        doesExist = true;
                        if (item.Level < constUpgrade.Level)
                        {
                            toReplace = true;
                            constUpgradeDataSaveToRemove = item;
                        }
                    }
                }
                if (doesExist)
                {
                    if (toReplace)
                    {
                        ConstUpgradeList.Remove(constUpgradeDataSaveToRemove);
                        ConstUpgradeList.Add(constUpgrade);
                    }
                }
                else
                {
                    ConstUpgradeList.Add(constUpgrade);
                }
            }
        }
    }
    public void SetUnlockedPlayerChoices(List<UnlockedPlayerChoiceDataSave> unlockedPlayerChoiceDataSaves)
    {
        UnlockedPlayerChoiceList = unlockedPlayerChoiceDataSaves;
    }
    public void SetUnlockedMapChoices(List<UnlockedMapChoiceData> unlockedMapChoiceDatas)
    {
        UnlockedMapChoiceList = unlockedMapChoiceDatas;
    }
    public void SetAudioMixerGroupsValue(List<AudioMixerGroupSaveData> audioMixerGroupSaveDatas)
    {
        AudioMixerGroupList = audioMixerGroupSaveDatas;
    }
}
[Serializable]
public class UnlockedPlayerChoiceDataSave
{
    public int Id;
    public bool IsPurchased = false;
    public UnlockedPlayerChoiceDataSave(int id) : this (id, false) { }
    public UnlockedPlayerChoiceDataSave(int id, bool isPurchased)
    {
        Id = id;
        IsPurchased = isPurchased;
    }
}
[Serializable]
public class UnlockedMapChoiceData
{
    public int Id;
    public bool IsPurchased = false;
    public UnlockedMapChoiceData(int id)
    {
        Id = id;
    }
}
[Serializable]
public class ConstUpgradeDataSave
{
    public int Level = 0;
    public int Id = 1;
    public ConstUpgradeDataSave(UpgradeSO upgradeSO)
    {
        Level = upgradeSO.Level;
        Id = upgradeSO.Id;
    }
}
[Serializable]
public class AudioMixerGroupSaveData
{
    public string Name;
    public float Volume;
    public AudioMixerGroupSaveData(string name) : this(name, 0f)
    {

    }
    public AudioMixerGroupSaveData(string name, float volume)
    {
        Name = name;
        Volume = volume;
    }
}
