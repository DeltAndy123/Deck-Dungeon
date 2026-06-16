using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 3.5f;
    [SerializeField] private float minDetectRadius = 2f;
    [SerializeField] private float maxDetectRadius = 8f;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private Transform waypointsParent;

    private enum State { Patrol, Chase }
    private State state = State.Patrol;

    private Rigidbody2D rb;
    private PlayerStats playerStats;
    private Transform playerTransform;
    private int waypointIndex;
    private float attackTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = FindAnyObjectByType<PlayerStats>();
        playerTransform = FindAnyObjectByType<PlayerController>().transform;
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        float t = playerStats.MaxClank > 0 ? playerStats.Clank / playerStats.MaxClank : 0f;
        float detectRadius = Mathf.Lerp(minDetectRadius, maxDetectRadius, t);
        float dist = Vector2.Distance(transform.position, playerTransform.position);

        if (dist <= detectRadius)
            state = State.Chase;
        else if (dist > detectRadius * 1.2f)
            state = State.Patrol;

        if (state == State.Chase && dist <= attackRadius && attackTimer <= 0f)
        {
            attackTimer = attackCooldown;
            playerStats.TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {
        if (state == State.Patrol)
            Patrol();
        else
            Chase();
    }

    private void Patrol()
    {
        if (waypointsParent == null || waypointsParent.childCount == 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        var target = waypointsParent.GetChild(waypointIndex).position;
        rb.linearVelocity = ((Vector2)(target - transform.position)).normalized * patrolSpeed;

        if (Vector2.Distance(transform.position, target) < 0.15f)
            waypointIndex = (waypointIndex + 1) % waypointsParent.childCount;
    }

    private void Chase()
    {
        rb.linearVelocity = ((Vector2)(playerTransform.position - transform.position)).normalized * chaseSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minDetectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDetectRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        if (waypointsParent == null || waypointsParent.childCount < 2) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            var wp = waypointsParent.GetChild(i);
            var next = waypointsParent.GetChild((i + 1) % waypointsParent.childCount);
            Gizmos.DrawWireSphere(wp.position, 0.15f);
            Gizmos.DrawLine(wp.position, next.position);
        }
    }
}
