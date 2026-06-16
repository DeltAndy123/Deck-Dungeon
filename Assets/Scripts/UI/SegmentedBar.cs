using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentedBar : MonoBehaviour
{
    [SerializeField] private Image segmentPrefab;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private readonly List<Image> segments = new();

    public void Init(int totalSegments)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        segments.Clear();

        for (var i = 0; i < totalSegments; i++)
        {
            var seg = Instantiate(segmentPrefab, transform);
            seg.color = inactiveColor;
            segments.Add(seg);
        }
    }

    public void SetValue(int current)
    {
        for (var i = 0; i < segments.Count; i++)
            segments[i].color = i < current ? activeColor : inactiveColor;
    }
}