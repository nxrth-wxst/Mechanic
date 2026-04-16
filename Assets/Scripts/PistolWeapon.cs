using UnityEngine;
using UnityEngine.InputSystem;

public class PistolWeapon : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject Pistol;
    [SerializeField] private float BulletPower = 100f;
    private Controls controls;

   
    void Awake()
    {
        controls = new Controls();
    }

 
    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.ShootPistol.performed += ShootTheGun;
    }

  
    void OnDisable()
    {
        controls.Player.ShootPistol.performed -= ShootTheGun;
        controls.Player.Disable();
    }

    private void ShootTheGun(InputAction.CallbackContext context)
    {
   
        if (Pistol == null) return;

        IBullet iBullet = GetComponent<IBullet>();
        iBullet.Shoot(BulletPower);
    }

    public void Shoot(float power) 
    {
        GameObject Bullet = Instantiate(Pistol, transform.position, transform.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(-transform.forward * power, ForceMode.Impulse);
        }
    }
}
