using System;
using UnityEngine;
using System.Collections;
public class CoinsManager : Savable
{
    public static CoinsManager instance;
    public int Coins { get; private set;}
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
    public void AddCoins(int coins)
    {
        Coins += coins;
        OnCoinsChanged?.Invoke(Coins);

    }
    public bool TrySpendCoins(int requiredAmount)
    {
        if (requiredAmount <= Coins)
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }
    public override void Load(DataSave dataSave)
    {
        return;
    }
    public override void Save(DataSave dataSave)
    {
        if (dataSave != null)
        {
            dataSave.SetCoins(dataSave.Coins + Coins);
        }
    }
}
