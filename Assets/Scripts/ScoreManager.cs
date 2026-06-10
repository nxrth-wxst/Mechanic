using System.Runtime.CompilerServices;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int currentScore = 0;
    [SerializeField] public int CurrentScore => currentScore;
    [SerializeField] public event System.Action<int> OnScoreChanged;
    private const int ScorePerKill = 10;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddKillScore()
    {
        currentScore += ScorePerKill;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void SpendMoney(float amount)
    {
        currentScore -= (int)amount;
        OnScoreChanged?.Invoke(currentScore);
    }

    public int GetMoney
    {
        get { return currentScore; }
        private set { currentScore = value; }
    }
}