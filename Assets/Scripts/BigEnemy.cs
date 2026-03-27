using UnityEngine;

public class BigEnemy : MonoBehaviour, ICollidable
{
    [SerializeField]private float dummyHealth = 45f;

    private const float DummyHealth = 0f;

    void ICollidable.OnCollision(BulletScript Bullet)
    {
        Debug.Log("bigwashit");
        dummyHealth -= Bullet.HMDamage;
    }

    public void Update()
    {
        if (dummyHealth < DummyHealth)
        {
            Destroy(gameObject);
        }
    }



}
