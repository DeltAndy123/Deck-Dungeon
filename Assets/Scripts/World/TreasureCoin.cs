using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TreasureCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerController _)) return;
        FindAnyObjectByType<PlayerStats>().AddCoins(1);
        Destroy(gameObject);
    }
}
