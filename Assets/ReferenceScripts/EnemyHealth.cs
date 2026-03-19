using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICollidable
{
    private float dummyHealth = 15f;
    
    
    void ICollidable.OnCollision(BulletScript Bullet)
    {
        dummyHealth -= Bullet.HMDamage;
    }

    public void Update()
    {
      if(dummyHealth < 0)
        {
            Destroy(gameObject);
        }
    }



}
