using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletpower = 6f;
    private Controls controls;
    private bool isShooting = false;

    void Awake()
    {
        controls = new Controls();
    }
    
    void OnEnable()
    {
  
        controls.Player.Enable();
        controls.Player.ShootAssault.started += ShootTheGun;
        controls.Player.ShootAssault.canceled -= ShootTheGun;
    }
    void OnDisable()
    {
        controls.Player.ShootAssault.started += ShootTheGun;
        controls.Player.ShootAssault.canceled -= ShootTheGun;
        controls.Player.Disable();
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        isShooting = true;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        isShooting = false;
    }
    private void ShootTheGun(InputAction.CallbackContext context)
    {
        IBullet iBullet = GetComponent<IBullet>();
        if (iBullet != null)
        {
            iBullet.Shoot(bulletpower);
        }
    }

    
    public void Shoot(float BulletPower)
    {
        if (bullet == null) return; 

        GameObject BulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody rb = BulletInstance.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (isShooting)
        {
            IBullet iBullet = GetComponent<IBullet>();
            if (iBullet != null)
            {
                iBullet.Shoot(bulletpower);
            }
        }

    }

}
