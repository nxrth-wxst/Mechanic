using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour, PColliable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float enemyContact = 10f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject GameOverPanel;

    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    private const float playerHealthZero = 0;
    private const float healthStart = 0;
    private float currentHealth;
    private const float TimeScalePaused = 0f;
    private const float TimeScaleNormal = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateSlider();
    }

    public void PlayerCollision(EnemyAI enemy)
    {
        TakeDamage(enemyContact);
    }

    private void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, healthStart, maxHealth);
        UpdateSlider();
        if (currentHealth <= playerHealthZero)
        {
            Die();
        }
    }

    private void UpdateSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        if (gameOverScoreText != null && ScoreManager.Instance != null)
        {
            gameOverScoreText.text = "Score: " + ScoreManager.Instance.CurrentScore;
        }

        GameOverPanel.SetActive(true);
        Time.timeScale = TimeScalePaused;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = TimeScaleNormal;
        ScoreManager.Instance?.ResetScore();
        SceneManager.LoadScene("MainAriefScene");
    }
}