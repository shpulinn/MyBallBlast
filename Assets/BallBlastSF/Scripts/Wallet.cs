using System;
using UnityEngine;

public class Wallet
{
    public const int MAX_BALANCE = 9999;
    
    private int balance;
    
    public event Action<int> OnBalanceChanged;
    
    public int Balance => balance;

    private Wallet(int initialBalance = 0)
    {
        balance = Mathf.Clamp(initialBalance, 0, MAX_BALANCE);
    }

    public void Deposit(int amount)
    {
        if (amount <= 0) return;
        
        balance = Math.Min(balance + amount, MAX_BALANCE);
        Save();
        OnBalanceChanged?.Invoke(balance);
    }

    public bool TryWithdraw(int amount)
    {
        if (amount > balance || amount < 1) return false;
        
        balance -= amount;
        Save();
        OnBalanceChanged?.Invoke(balance);
        return true;
    }
    
    private void Save()
    {
        PlayerPrefs.SetInt("Coins", balance);
    }

    public static Wallet Load(int defaultBalance = 0)
    {
        int saved = PlayerPrefs.GetInt("Coins", defaultBalance);
        return new Wallet(saved);
    }
}