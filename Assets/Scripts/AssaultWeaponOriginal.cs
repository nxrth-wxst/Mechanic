using UnityEngine;

public class AssaultWeapon : MonoBehaviour, IBullet
{
    public GameObject Bullet;
    public float BulletPower = 6f;

    public void Shoot(float BulletPower)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            IBullet iBullet = GetComponent<IBullet>();
            iBullet.Shoot(BulletPower);
        }
    }
}

