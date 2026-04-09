using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletpower = 6f;
    private Controls controls;

   
    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.ShootAssault.performed += ShootTheGun;
    }

   
    void OnDisable()
    {
        controls.Player.ShootAssault.performed -= ShootTheGun;
        controls.Player.Disable();
    }

    private void ShootTheGun(InputAction.CallbackContext context)
    {
        // Interface check and execution
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
}
