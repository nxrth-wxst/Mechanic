using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PistolWeapon : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject PistolBullet;
    private float BulletPower = 100f;
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
