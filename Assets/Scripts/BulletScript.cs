using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    private float Damage;
    private const float BulletDamage = 1f;

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
        Damage = BulletDamage;
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
       get { return Damage; }
    }





}
