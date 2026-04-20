using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject Assault;
    [SerializeField] private float bulletpower = 6f;
    [SerializeField] private float fireRate = 0.1f;
    private Controls controls;
<<<<<<< HEAD:Assets/Scripts/AssaultWeapon.cs

    private bool isShooting = false;
    public event EventHandler OnFire;
=======
    private bool isShooting = false;
    private float nextFire;
    public event EventHandler OnFire;
    
>>>>>>> 5403befa0db7ab4abad3fc50df5a5fcf0de67a0f:Assets/Scripts/AssaultWeaponOriginal.cs


    void Awake()
    {
        controls = new Controls();
    }
    
    void OnEnable()
    {
  
        controls.Player.Enable();
        controls.Player.ShootAssault.started += OnShootStarted;
        controls.Player.ShootAssault.canceled += OnShootCanceled;
    }
    void OnDisable()
    {
        controls.Player.ShootAssault.started -= OnShootStarted;
        controls.Player.ShootAssault.canceled -= OnShootCanceled;
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
    //private void ShootTheGun(InputAction.CallbackContext context)
    //{
    //    IBullet iBullet = GetComponent<IBullet>();
    //    if (iBullet != null)
    //    {
    //        iBullet.Shoot(bulletpower);
    //        OnFire?.Invoke(this,EventArgs.Empty);
    //    }
    //}

    private void Update()
    {
        if (isShooting && Time.time >= nextFire)
        {
            IBullet iBullet = GetComponent<IBullet>();
            if (iBullet != null)
            {
                iBullet.Shoot(bulletpower);
                nextFire = Time.time + fireRate;
            }
        }

    }
    public void Shoot(float BulletPower)
    {
        if (Assault == null) return;
        OnFire?.Invoke(this, EventArgs.Empty);

        GameObject BulletInstance = Instantiate(Assault, transform.position, transform.rotation);
        Rigidbody rb = BulletInstance.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse);
        }
    }

    

}
