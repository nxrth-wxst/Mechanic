using UnityEngine;

public class PistolWeapon : MonoBehaviour, IBullet
{
    public GameObject Bullet;
    public float BulletPower = 6f;
    public void PistolShoot(float BulletPower1)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation);  //Clones the bullet
        Rigidbody rb = Bullet.GetComponent<Rigidbody>(); //Rigidbody so the bullet can move
        rb.AddForce(-transform.forward * BulletPower1, ForceMode.Impulse); //bullet launching
    }
    public void Shoot(float BulletPower)
    {
         
    }


    void Update()
    {
        IBullet iBullet = GetComponent<IBullet>();

        if (Input.GetButtonDown("Fire2"))
        {
            iBullet.PistolShoot(BulletPower);
        }
    }
}
