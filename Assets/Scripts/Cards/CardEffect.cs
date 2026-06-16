using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public abstract void Apply(PlayerStats player);
}