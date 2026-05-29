using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, ICollidable
{
    private float dummyHealth = 2f;
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
