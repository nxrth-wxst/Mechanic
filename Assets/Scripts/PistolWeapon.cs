using UnityEngine;

public class PistolWeapon : MonoBehaviour, IBullet
{
    public GameObject Bullet;
    public float BulletPower = 6f;

    void IBullet.Shoot(float PistolShoot)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation);  //Clones the bullet
        Rigidbody rb = Bullet.GetComponent<Rigidbody>(); //Rigidbody so the bullet can move
        rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse); //bullet launching

    }

    void Update()
    {
        IBullet iBullet = GetComponent<IBullet>();

        if (Input.GetButtonDown("Fire2"))
        {
            iBullet.Shoot(BulletPower);
        }
    }
}
