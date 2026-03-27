using UnityEngine;

public class PlayerHealth : MonoBehaviour, PColliable
{
    [SerializeField] private float playerHealth = 10f;
    
    
    void PColliable.PlayerCollision(EnemyDamage enemyDamage)
    {
        playerHealth -= 1f;
    }

    
}
