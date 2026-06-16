using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/AddClank")]
public class AddClankEffect : CardEffect
{
    public float amount = 1f;
    public float duration = 0f;

    public override void Apply(PlayerStats player)
    {
        player.AddClank(amount, duration);
    }
}
