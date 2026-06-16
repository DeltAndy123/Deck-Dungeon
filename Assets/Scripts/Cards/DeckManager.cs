using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private float drawInterval = 30f;
    [SerializeField] private List<CardData> startingDeck = new();
    [SerializeField] private CardData stumbleCard;
    [SerializeField] private float stumbleInterval = 120f;

    private List<CardData> deck = new();
    private float timer;
    private float stumbleTimer;
    
    public event Action<int> OnDeckChanged;
    public event Action<CardData> OnCardDrawn;

    public void LoadDeck(List<CardData> cards)
    {
        deck = new List<CardData>(cards);
        Shuffle(deck);
    }

    private void Start()
    {
        LoadDeck(startingDeck);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= drawInterval)
        {
            timer = 0;
            DrawCard();
        }

        stumbleTimer += Time.deltaTime;
        if (stumbleTimer >= stumbleInterval)
        {
            stumbleTimer = 0;
            InjectStumble();
        }
    }

    private void InjectStumble()
    {
        if (stumbleCard == null) return;
        deck.Add(stumbleCard);
        Shuffle(deck);
        Debug.Log($"Injected {stumbleCard.cardName}");
    }

    private void DrawCard()
    {
        if (deck.Count == 0) return;
        var card = deck[0];
        deck.RemoveAt(0);
        var player = FindAnyObjectByType<PlayerStats>();
        print($"Drawing card: {card.name}");
        foreach (var effect in card.effects)
            effect.Apply(player);
        Shuffle(deck); // Decked Out reshuffles on every draw
        OnCardDrawn?.Invoke(card);
        OnDeckChanged?.Invoke(deck.Count);
    }

    private void Shuffle<T>(List<T> list)
    {
        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}