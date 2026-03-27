using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICollidable
{
    private float dummyHealth = 15f;
    private WaveSystem waveManager;

    void ICollidable.OnCollision(BulletScript Bullet)
    {
        dummyHealth -= Bullet.HMDamage;
    }

    public void Update()
    {
      if(dummyHealth < 0)
        {
            Destroy(gameObject);
            Dead();
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
