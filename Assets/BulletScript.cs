using UnityEngine;

public class BulletScript : MonoBehaviour
{


    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        
        ICollidable collidable = other.GetComponent<ICollidable>();
        if (collidable != null)
        {

            collidable.OnCollision(this);
           
            
            
            
            
            
            
            //EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            //if (enemyHealth != null)
            //{
            //    enemyHealth.dummyHealth -= 1f;
            //}

            //BigEnemy bigEnemy = other.GetComponent<BigEnemy>();
            //if (bigEnemy != null)
            //{
            //    bigEnemy.dummyHealth -= 1f;
            //}
        }
    
    
    
    
    }


    private void Awake()
    {
        Damage = 1.0f;
    }



}
