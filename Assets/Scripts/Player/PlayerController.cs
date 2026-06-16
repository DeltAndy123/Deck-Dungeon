using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private VirtualJoystick joystick;
    private Rigidbody2D rb;
    private InputAction moveAction;
    private PlayerStats stats;
    private ArtifactPickup nearbyArtifact;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = GetComponent<PlayerInput>().actions["Move"];
        stats = FindAnyObjectByType<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ArtifactPickup artifact))
            nearbyArtifact = artifact;

        if (other.TryGetComponent(out ExitZone exit) && stats.HasArtifact)
            exit.Escape(stats);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out ArtifactPickup artifact) && artifact == nearbyArtifact)
            nearbyArtifact = null;
    }

    private void FixedUpdate()
    {
        // Joystick takes priority, falls back to keyboard
        Vector2 input = joystick.InputValue != Vector2.zero
            ? joystick.InputValue
            : moveAction.ReadValue<Vector2>();

        rb.linearVelocity = input * speed;
    }

    public void AttemptExtract()
    {
        if (nearbyArtifact == null) return;
        nearbyArtifact.Collect(stats);
        nearbyArtifact = null;
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed /= multiplier;
    }
}