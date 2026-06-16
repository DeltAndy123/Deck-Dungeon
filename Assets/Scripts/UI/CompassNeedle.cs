using UnityEngine;

public class CompassNeedle : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform artifactTarget;
    [SerializeField] private Transform exitTarget;
    private PlayerStats stats;

    private void Awake()
    {
        stats = FindAnyObjectByType<PlayerStats>();
    }

    void Update()
    {
        Transform target = stats.HasArtifact ? exitTarget : artifactTarget;
        Vector3 direction = target.position - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 5f);
    }
}
