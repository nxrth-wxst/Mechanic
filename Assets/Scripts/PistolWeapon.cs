using UnityEngine;
using static UnityEngine.UI.Image;

public class PistolWeapon : MonoBehaviour, IBullet
{
    public GameObject PistolBullet;
    public float BulletPower = 6f;
    public float timer;

    void IBullet.Shoot(float PistolShoot)
    {
        GameObject Bullet = Instantiate(this.PistolBullet, transform.position, transform.rotation);  //Clones the bullet
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
