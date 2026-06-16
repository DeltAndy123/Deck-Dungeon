using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int treasure = 0;
    [SerializeField] private int stealth = 0;
    [SerializeField] private int embers = 0;
    [SerializeField] private float clank = 0f;
    [SerializeField] private int health = 3;
    
    [SerializeField] private int maxTreasure = 10;
    [SerializeField] private int maxStealth = 10;
    [SerializeField] private int maxClank = 10;
    [SerializeField] private int maxHealth = 3;

    [SerializeField] private int coins = 0;

    public event Action<int> OnTreasureChanged;
    public event Action<int> OnStealthChanged;
    public event Action<float> OnClankChanged;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnCoinsChanged;

    public int Treasure => treasure;
    public int Stealth => stealth;
    public float Clank => clank;
    public int Health => health;
    public int Coins => coins;
    public bool HasArtifact { get; private set; }

    public int MaxTreasure => maxTreasure;
    public int MaxStealth => maxStealth;
    public int MaxClank => maxClank;
    public int MaxHealth => maxHealth;

    public void AddTreasure(int amount)
    {
        treasure += amount;
        OnTreasureChanged?.Invoke(treasure);
    }

    public void ConsumeTreasure()
    {
        if (treasure <= 0) return;
        treasure--;
        OnTreasureChanged?.Invoke(treasure);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinsChanged?.Invoke(coins);
    }

    public void AddStealth(int amount)
    {
        stealth += amount;
        OnStealthChanged?.Invoke(stealth);
    }

    public void AddClank(float amount, float duration = 0f)
    {
        int blocked = Mathf.Min(stealth, Mathf.FloorToInt(amount));
        if (blocked > 0)
        {
            stealth -= blocked;
            amount -= blocked;
            OnStealthChanged?.Invoke(stealth);
        }

        if (amount <= 0) return;

        if (duration <= 0f)
        {
            clank = Mathf.Min(clank + amount, maxClank);
            OnClankChanged?.Invoke(clank);
            return;
        }

        StartCoroutine(AnimateClank(amount, duration));
    }

    private IEnumerator AnimateClank(float amount, float duration)
    {
        var applied = 0f;
        while (applied < amount)
        {
            var delta = Mathf.Min(amount * Time.deltaTime / duration, amount - applied);
            applied += delta;
            clank = Mathf.Min(clank + delta, maxClank);
            OnClankChanged?.Invoke(clank);
            yield return null;
        }
    }

    public void CollectArtifact()
    {
        HasArtifact = true;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnHealthChanged?.Invoke(health);
    }
}