using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    [SerializeField] private Color color = Color.yellow;
    [SerializeField] private float radius = 0.5f;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}