using System;
using UnityEngine;
using System.Collections;

public class CoinsManagerMainMenu : Savable
{
    public static CoinsManagerMainMenu instance;
    public int Coins {get; private set;} = 0;
    public event Action<int> OnCoinsChanged;
    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SaveManager.LoadGame();
    }
    public void AddCoins(int value)
    {
        if (value > 0) { 
            Coins += value;
            OnCoinsChanged?.Invoke(Coins);
        }
    }
    public bool TrySpendCoins(int value)
    {
        bool isAbleToSpendCoins = Coins >= value;
        if (isAbleToSpendCoins)
        {
            SpendCoins(value);
        }
        return isAbleToSpendCoins;
    }
    public void SpendCoins(int value)
    {
        if (Coins >= value)
        {
            Coins -= value;
            OnCoinsChanged?.Invoke(Coins);
        }
        else
        {
            Debug.Log("Not enough coins to purchase");
        }
    }
    public override void Load(DataSave dataSave)
    {
        if (dataSave != null)
        {
            Coins = dataSave.Coins;
            OnCoinsChanged?.Invoke(Coins);
        }
    }
    public override void Save(DataSave dataSave)
    {
        if (dataSave != null)
        {
            dataSave.SetCoins(Coins);
        }
    }
}
