using System;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeStats : MonoBehaviour
{
    public static RuntimeStats Instance { get; private set; }
    public Dictionary<string, int> KilledEnemiesDictionary { get; private set; } = new();
    public int XpGained { get; private set; } = 0;
    public event Action OnStatsChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddKilledEnemy(string enemyName)
    {
        if (KilledEnemiesDictionary.TryGetValue(enemyName, out var amount))
        {
            KilledEnemiesDictionary[enemyName] = amount + 1;
        }
        else
        {
            KilledEnemiesDictionary.Add(enemyName, 1);
        }
        OnStatsChanged?.Invoke();
    }
    public string GetKilledEnemiesToString()
    {
        if (KilledEnemiesDictionary.Count == 0)
        {
            return "No enemies have been killed" + this;
        }
        string KilledEnemiesText = "Enemies Killed: \n";
        foreach (var item in KilledEnemiesDictionary)
        {
            KilledEnemiesText += $"{item.Key} : {item.Value}\n";
        }
        return KilledEnemiesText;
    }
    public void AddXP(int xp)
    {
        XpGained += xp;
    }
}
