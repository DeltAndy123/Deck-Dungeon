using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/AddStealth")]
public class AddStealthEffect : CardEffect
{
    public int amount = 3;

    public override void Apply(PlayerStats player)
    {
        player.AddStealth(amount);
    }

    // public override string GetDescription() => $"Gain {amount} Stealth.";
}