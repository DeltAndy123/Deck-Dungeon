using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private SegmentedBar healthBar;
    [SerializeField] private SegmentedBar treasureBar;
    [SerializeField] private SegmentedBar stealthBar;
    [SerializeField] private SegmentedBar cardsBar;
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private Image clankBar;
    [SerializeField] private Color clankLowColor = Color.green;
    [SerializeField] private Color clankHighColor = Color.red;

    private PlayerStats stats;

    private void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();
        var deck = FindAnyObjectByType<DeckManager>();
        
        deck.LoadDeck(deck.startingDeck);

        stats.OnHealthChanged += UpdateHealth;
        deck.OnDeckChanged += UpdateCards;
        stats.OnTreasureChanged += UpdateTreasure;
        stats.OnStealthChanged += UpdateStealth;
        stats.OnCoinsChanged += UpdateCoins;
        stats.OnClankChanged += UpdateClank;

        healthBar.Init(stats.MaxHealth);
        cardsBar.Init(deck.TotalCards);
        treasureBar.Init(stats.MaxTreasure);
        stealthBar.Init(stats.MaxStealth);
        UpdateHealth(stats.Health);
        UpdateCards(deck.RemainingCards);
        UpdateTreasure(stats.Treasure);
        UpdateStealth(stats.Stealth);
        UpdateCoins(stats.Coins);
        UpdateClank(stats.Clank);
    }

    private void UpdateHealth(int health)
    {
        healthBar.SetValue(health);
    }
    
    private void UpdateTreasure(int treasure)
    {
        treasureBar.SetValue(treasure);
    }

    private void UpdateStealth(int stealth)
    {
        stealthBar.SetValue(stealth);
    }

    private void UpdateCoins(int coins)
    {
        if (coinsText != null) coinsText.text = coins.ToString();
    }

    private void UpdateCards(int remaining)
    {
        cardsBar.SetValue(remaining);
    }

    private void UpdateClank(float clank)
    {
        var t = clank / stats.MaxClank;
        clankBar.fillAmount = t;
        clankBar.color = Color.Lerp(clankLowColor, clankHighColor, t);
    }
}