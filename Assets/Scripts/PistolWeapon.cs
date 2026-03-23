using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PistolWeapon : MonoBehaviour, IBullet
{
    public GameObject PistolBullet;
    public float BulletPower = 6f;
    private Controls controls;

    void IBullet.Shoot(float PistolShoot)
    {
        GameObject Bullet = Instantiate(this.PistolBullet, transform.position, transform.rotation);  //Clones the bullet
        Rigidbody rb = Bullet.GetComponent<Rigidbody>(); //Rigidbody so the bullet can move
        rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse); //bullet launching

     
    }

    void Start()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.ShootPistol.performed += ShootTheGun;

    }

    private void ShootTheGun(InputAction.CallbackContext context)
    {
        if (!isActiveAndEnabled) return; 
        IBullet iBullet = GetComponent<IBullet>();
        iBullet.Shoot(BulletPower);
    }
}
