using UnityEngine;

public class ArtifactSpawner : MonoBehaviour
{
    [SerializeField] private Transform artifact;
    [SerializeField] private Transform spawnPointsParent;

    private void Start()
    {
        if (spawnPointsParent == null || spawnPointsParent.childCount == 0) return;
        var point = spawnPointsParent.GetChild(Random.Range(0, spawnPointsParent.childCount));
        artifact.position = point.position;
    }
}
