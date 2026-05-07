using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICollidable
{
    private float dummyHealth = 15f;
    private WaveSystem waveManager;
    
    void ICollidable.OnCollision(float damage)
    {
        dummyHealth -= damage;

        if (dummyHealth < 0)
        {
            Dead();
            Destroy(gameObject);
            
        }

    }

    public void SetWaveManager(WaveSystem Manager)
    {
    waveManager = Manager;
    
    
    }
    
    private void Dead()
    {
      
        
        
        if (waveManager != null)
        {
            waveManager.OnEnemyDied();
        }
    }
 
}
