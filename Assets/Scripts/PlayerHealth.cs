using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, PColliable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float enemyContact = 10f;
    [SerializeField] private Slider healthSlider;

    private const float PlayerHealthZero = 0;
    private const float HealthStart = 0;

    private float currentHealth;

    [SerializeField] private GameObject GameOverPanel;

    private const float TimeScalePaused = 0f;
    private const float TimeScaleNormal = 1f;

    private void Start() //starts with full hp
    {
        currentHealth = maxHealth;
        UpdateSlider();
    }

    public void PlayerCollision(EnemyDamage enemy) //the collision making the slider work
    {
        TakeDamage(enemyContact);
    }

    private void TakeDamage(float amount) //if the enemy hits player it loses hp
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, HealthStart, maxHealth); UpdateSlider();

        if (currentHealth <= PlayerHealthZero)
        {
            Die();
        }
    }

    private void UpdateSlider() //updating the ui 
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    private void Die() //if the health is 0, shows gameoverpanel
    {
         GameOverPanel.SetActive(true);
     
         Time.timeScale = TimeScalePaused;
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
    }

}
