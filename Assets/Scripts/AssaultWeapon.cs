using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float BulletPower = 6f;
    private Controls controls;
    void IBullet.Shoot(float BulletPower)
    {
        GameObject Bullet = Instantiate(this.Bullet, transform.position, transform.rotation); //Clones the bullet
        Rigidbody rb = Bullet.GetComponent<Rigidbody>(); //Rigidbody so the bullet can move
        
        rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse); //bullet launching
        
    }


    void Start()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.ShootAssault.performed += ShootTheGun;

    }

    private void ShootTheGun(InputAction.CallbackContext context)
    {
        if (!isActiveAndEnabled) return;
        IBullet iBullet = GetComponent<IBullet>();
        iBullet.Shoot(BulletPower);
    }


}