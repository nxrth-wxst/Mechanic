using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int currentScore = 0;
    [SerializeField] public int CurrentScore => currentScore; //has to be public for reference to another script
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
    public void AddKillScore() //has to be public for reference to another script
    {
        currentScore += ScorePerKill;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void ResetScore() //has to be public for reference to another script
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }
}
