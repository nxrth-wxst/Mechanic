using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float BulletPower = 6f;
  //  private Controls controls;
    void IBullet.Shoot(float BulletPower)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation); //Clones the bullet
        Rigidbody rb = Bullet.GetComponent<Rigidbody>(); //Rigidbody so the bullet can move
        
        rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse); //bullet launching
        
    }


    void Update()
    {
       // controls = new Controls();
     //   controls.Player.Enable();
       // controls.Player.Shoot.performed += ShootTheGun;


       IBullet iBullet = GetComponent<IBullet>();
       if (Input.GetButtonDown("Fire1"))
       {

          iBullet.Shoot(BulletPower);
       }

    }

  //  private void ShootTheGun(InputAction.CallbackContext context)
   // {
   //     IBullet iBullet = GetComponent<IBullet>();
   //     iBullet.Shoot(BulletPower);
   // }


}