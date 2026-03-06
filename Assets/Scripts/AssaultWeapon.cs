using UnityEngine;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    public GameObject Bullet;
    public float BulletPower = 6f;

    public void Shoot(float BulletPower)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation); //Clones the bullet
        Rigidbody rb = Bullet.GetComponent<Rigidbody>(); //Rigidbody so the bullet can move
        rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse); //bullet launching
    }

    public void PistolShoot(float BulletPower1)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * BulletPower1, ForceMode.Impulse);
    }

    void Update()
    {
        IBullet iBullet = GetComponent<IBullet>();

        if (Input.GetButtonDown("Fire1"))
        {
          
            iBullet.Shoot(BulletPower);
        }
        
    }
}