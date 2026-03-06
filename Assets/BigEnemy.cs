using UnityEngine;

public class BigEnemy : MonoBehaviour, ICollidable
{
    public float dummyHealth = 45f;


    void ICollidable.OnCollision(BulletScript Bullet)
    {
        Debug.Log("bigwashit");
        dummyHealth -= Bullet.Damage;
    }

    public void Update()
    {
        if (dummyHealth < 0)
        {
            Destroy(gameObject);
        }
    }



}
