using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/AddTreasure")]
public class AddTreasureEffect : CardEffect
{
    public int amount = 3;

    public override void Apply(PlayerStats player)
    {
        player.AddTreasure(amount);
    }

    // public override string GetDescription() => $"Gain {amount} Treasure.";
}