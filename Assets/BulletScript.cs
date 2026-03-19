using System.Collections;
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
           
            
            
            Destroy(gameObject);
            
            
            
            
        }
    
    
    
    
    }


    private void Awake()
    {
        Damage = 1.0f;
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







}
