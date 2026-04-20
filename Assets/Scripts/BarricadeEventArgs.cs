using UnityEngine;
using System;
public class BarricadeEventArgs : EventArgs
{
    public int BoardIndex { get; }
    public Barricade Barricade { get; }

    public BarricadeEventArgs(int boardIndex, Barricade barricade)
    {
        BoardIndex = boardIndex;
        Barricade = barricade;
    }


}

public class BoardDamagedEventArgs : BarricadeEventArgs
{
    public float PreviousHealth { get; }
    public float CurrentHealth { get; }
    public float DamageTaken => PreviousHealth - CurrentHealth;

public BoardDamagedEventArgs(int boardIndex, Barricade barricade,
                                  float previousHealth, float currentHealth)
        : base(boardIndex, barricade)
    {
        PreviousHealth = previousHealth;
        CurrentHealth = currentHealth;
    }
}

public class BoardRepairedEventArgs : BarricadeEventArgs
{
    public float HealthRestored { get; }
    public bool WasFullyRepaired { get; }

    public BoardRepairedEventArgs(int boardIndex, Barricade barricade,
                                   float healthRestored, bool wasFullyRepaired)
        : base(boardIndex, barricade)
    {
        HealthRestored = healthRestored;
        WasFullyRepaired = wasFullyRepaired;
    }
}
public class BarricadeBrokenEventArgs : BarricadeEventArgs
{
    public UnityEngine.Vector3 BreachPosition { get; }

    public BarricadeBrokenEventArgs(int boardIndex, Barricade barricade,
                                     UnityEngine.Vector3 breachPosition)
        : base(boardIndex, barricade)
    {
        BreachPosition = breachPosition;
    }
}