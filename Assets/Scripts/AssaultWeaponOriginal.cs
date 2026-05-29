using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

public class AssaultWeaponOriginal : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject Assault;
    [SerializeField] private float bulletpower = 6f;
    [SerializeField] private float fireRate = 0.1f;
    private Controls controls;
    private bool isShooting = false;
    private float nextFire;
    public event EventHandler OnFire;

    [SerializeField] private int currentAmmo = 30;
    [SerializeField] private int magCapacity = 30;
    [SerializeField] private float ReloadTime = 2.5f;

    [SerializeField] private TextMeshProUGUI ammoText; 

    private bool isReloading = false;

    private const float NoAmmo = 0;

    // public ParticleSystem muzzleFlash;  //gets the particlesystem in the gun

    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        if (controls == null) controls = new Controls();
        controls.Player.Enable(); 

        controls.Player.ShootAssault.started += OnShootStarted;
        controls.Player.ShootAssault.canceled += OnShootCanceled;
        controls.Player.Reload.performed += OnReloadPressed;

        UpdateAmmoUI(); 
    }

    void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.ShootAssault.started -= OnShootStarted;
        controls.Player.ShootAssault.canceled -= OnShootCanceled;
        controls.Player.Reload.performed -= OnReloadPressed;

      
        isReloading = false;
    }

    private void Start()
    {
        UpdateAmmoUI();
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        isShooting = true;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        isShooting = false;
    }


    private void OnReloadPressed(InputAction.CallbackContext context)
    {

        if (!isReloading && currentAmmo < magCapacity)
        {
            StartCoroutine(Reload());
        }
    }

    private void Update()
    {
        if (currentAmmo <= NoAmmo || isReloading)
        {
            return;
        }

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

    IEnumerator Reload()
    {
        isReloading = true;
      
        yield return new WaitForSeconds(ReloadTime);
        currentAmmo = magCapacity;
        isReloading = false;

        UpdateAmmoUI();
    }

    public void Shoot(float BulletPower)
    {
        currentAmmo--;
        UpdateAmmoUI();

        if (Assault == null) return;
        OnFire?.Invoke(this, EventArgs.Empty);
        GameObject BulletInstance = Instantiate(Assault, transform.position, transform.rotation);
        Rigidbody rb = BulletInstance.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(-transform.forward * BulletPower, ForceMode.Impulse);
        }
    }
    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {magCapacity}";
        }
    }
}

 