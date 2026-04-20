using UnityEngine;

public enum BoardState
{
    Intact,     
    Damaged,    
    Broken     
}

[System.Serializable]
public class Board
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
    [field: SerializeField] public float CurrentHealth { get; private set; }

    public BoardState State { get; private set; }
    public bool IsBroken => State == BoardState.Broken;

    public Board(float maxHealth = 100f)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        State = BoardState.Intact;
    }

    public float TakeDamage(float amount)
    {
        if (IsBroken) return 0f;

        float before = CurrentHealth;
        CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);
        UpdateState();
        return before - CurrentHealth; 
    }

    public float Repair(float amount)
    {
        if (!IsBroken && CurrentHealth >= MaxHealth) return 0f;

        float before = CurrentHealth;
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        State = CurrentHealth >= MaxHealth ? BoardState.Intact : BoardState.Damaged;
        return CurrentHealth - before;
    }

    private void UpdateState()
    {
        if (CurrentHealth <= 0f) State = BoardState.Broken;
        else if (CurrentHealth < MaxHealth * 0.5f) State = BoardState.Damaged;
        else State = BoardState.Intact;
    }

}
