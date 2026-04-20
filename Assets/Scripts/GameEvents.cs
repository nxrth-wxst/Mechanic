using System;
using UnityEngine;

public class PlankEventArgs : EventArgs
{
    public int CurrentPlanks { get; }
    public int MaxPlanks { get; }
    public GameObject Barricade { get; }

    public PlankEventArgs(int current, int max, GameObject barricade)
    {
        CurrentPlanks = current;
        MaxPlanks = max;
        Barricade = barricade;
    }
}

public class BarricadeDestroyedEventArgs : EventArgs
{
    public GameObject Barricade { get; }
    public Vector3 Position { get; }

    public BarricadeDestroyedEventArgs(GameObject barricade, Vector3 position)
    {
        Barricade = barricade;
        Position = position;
    }
}

public class RepairEventArgs : EventArgs
{
    public GameObject Barricade { get; }
    public float Progress { get; }   // 0-1, useful for a UI progress bar

    public RepairEventArgs(GameObject barricade, float progress)
    {
        Barricade = barricade;
        Progress = progress;
    }
}
