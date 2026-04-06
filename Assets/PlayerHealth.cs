using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, PColliable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float enemyContact = 10f;
    [SerializeField] private Slider healthSlider;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateSlider();
    }

    public void PlayerCollision(EnemyDamage enemy)
    {
        TakeDamage(enemyContact);
    }

    private void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0f, maxHealth); UpdateSlider();

        if (currentHealth <= 0f)
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
        Debug.Log("dead");
    }
}
