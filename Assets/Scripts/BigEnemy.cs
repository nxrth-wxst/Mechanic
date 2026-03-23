using UnityEngine;

public class BigEnemy : MonoBehaviour, ICollidable
{
    [SerializeField]private float dummyHealth = 45f;


    void ICollidable.OnCollision(BulletScript Bullet)
    {
        Debug.Log("bigwashit");
        dummyHealth -= Bullet.HMDamage;
    }

    public void Update()
    {
        if (dummyHealth < 0)
        {
            Destroy(gameObject);
        }
    }



}
