using UnityEngine;

[CreateAssetMenu(menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string description;
    public Sprite artwork;
    public CardEffect[] effects;    // Effects applied when this card is drawn
    public int maxCopiesInDeck = 3;
    // public CardRarity rarity;
    public int cost;                // Shop cost in Frost Embers
}