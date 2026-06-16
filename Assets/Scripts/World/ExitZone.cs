using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExitZone : MonoBehaviour
{
    private RunManager runManager;
    
    public void Start()
    {
        runManager = FindAnyObjectByType<RunManager>();
    }

    public void Escape(PlayerStats player)
    {
        runManager.EndRun(true);
    }
}
