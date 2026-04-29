using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject Assault;
    [SerializeField] private float bulletpower = 6f;
    [SerializeField] private float fireRate = 0.1f;
    private Controls controls;
    private bool isShooting = false;
    private float nextFire;
    public event EventHandler OnFire;

   // public ParticleSystem muzzleFlash;  //gets the particlesystem in the gun
    
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
        controls.Player.Disable();
        controls.Player.ShootAssault.started -= OnShootStarted;
        controls.Player.ShootAssault.canceled -= OnShootCanceled;

    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        isShooting = true;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        isShooting = false;
    }

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
       // muzzleFlash.Play();
        GameObject BulletInstance = Instantiate(Assault, transform.position, transform.rotation);
        Rigidbody rb = BulletInstance.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse);
        }
    }

}

