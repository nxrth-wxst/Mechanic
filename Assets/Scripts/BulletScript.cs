using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    private float damage;
    private const float bulletDamage = 1f;

    private void OnTriggerEnter(Collider other)
    {
        
        ICollidable collidable = other.GetComponent<ICollidable>();
        if (collidable != null)
        {

            collidable.OnCollision(this);
           
            
            
            Destroy(gameObject);
            
            
            
            
        }
    
    
    
    
    }


    private void Awake()
    {
        damage = bulletDamage;
    }

    private void Update()
    {
        StartCoroutine(DeleteBullet());
    }

    private IEnumerator DeleteBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public float HMDamage
    {
       get { return damage; }
    }





}
