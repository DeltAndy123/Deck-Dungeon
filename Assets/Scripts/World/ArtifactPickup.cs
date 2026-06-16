using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ArtifactPickup : MonoBehaviour
{
    [SerializeField] private float clankOnPickup = 3f;
    [SerializeField] private float clankRampDuration = 3f;

    public void Collect(PlayerStats player)
    {
        player.AddClank(clankOnPickup, clankRampDuration);
        player.CollectArtifact();
        // GetComponent<SpriteRenderer>().enabled = false;
        // GetComponent<Collider2D>().enabled = false;
    }
}
