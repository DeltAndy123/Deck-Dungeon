using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/AddSpeed")]
public class AddSpeedEffect : CardEffect
{
    public float multiplier = 1.5f;
    public float duration = 60f;

    public override void Apply(PlayerStats player)
    {
        UnityEngine.Object.FindAnyObjectByType<PlayerController>().ApplySpeedBoost(multiplier, duration);
    }
}
