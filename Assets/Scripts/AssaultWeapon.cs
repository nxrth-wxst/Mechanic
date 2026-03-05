using UnityEngine;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    public GameObject Bullet;
    public float BulletPower = 6f;

    public void Shoot(float BulletPower)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation); //Bullet cloning
        Rigidbody rb = Bullet.GetComponent<Rigidbody>(); //Bullets Rigidbody
        rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse); //Power of the bullet
    }

    void Update() //IBullet Interface so it can shoot
    {
        if (Input.GetButtonDown("Fire1"))
        {
            IBullet iBullet = GetComponent<IBullet>();
            iBullet.Shoot(BulletPower);
        }
    }
}

