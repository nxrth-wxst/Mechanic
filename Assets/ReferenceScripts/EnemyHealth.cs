using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICollidable
{
    public float dummyHealth = 15f;
    
    
    void ICollidable.OnCollision(BulletScript Bullet)
    {
        dummyHealth -= Bullet.Damage;
    }

    public void Update()
    {
      if(dummyHealth < 0)
        {
            Destroy(gameObject);
        }
    }



}
