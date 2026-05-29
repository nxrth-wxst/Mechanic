using System;
using System.Collections; 
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PistolWeapon : MonoBehaviour, IBullet
{
    [SerializeField] private GameObject Pistol;
    [SerializeField] private float BulletPower = 100f;
    private Controls controls;
    public event EventHandler OnClick;

    [SerializeField] private int currentAmmo = 12; 
    [SerializeField] private int magCapacity = 12;
    [SerializeField] private float ReloadTime = 1.5f; 

    [SerializeField] private TextMeshProUGUI ammoText; 

    private bool isReloading = false;

    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        if (controls == null) controls = new Controls();
        controls.Player.Enable();

        controls.Player.ShootPistol.performed += ShootTheGun;
        controls.Player.Reload.performed += OnReloadPressed;

        UpdateAmmoUI(); 
    }

    void OnDisable()
    {
        controls.Player.ShootPistol.performed -= ShootTheGun;
        controls.Player.Reload.performed -= OnReloadPressed;

      
        isReloading = false;
    }

    private void OnReloadPressed(InputAction.CallbackContext context)
    {
  
        if (!isReloading && currentAmmo < magCapacity)
        {
            StartCoroutine(Reload());
        }
    }

    private void ShootTheGun(InputAction.CallbackContext context)
    {
        if (Pistol == null) return;

    
        if (currentAmmo <= 0 || isReloading) return;

        IBullet iBullet = GetComponent<IBullet>();
        iBullet.Shoot(BulletPower);
    }

    public void Shoot(float power)
    {
        currentAmmo--;
        UpdateAmmoUI(); 

        GameObject Bullet = Instantiate(Pistol, transform.position, transform.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        OnClick?.Invoke(this, EventArgs.Empty);
        if (rb != null)
        {
            rb.AddForce(-transform.forward * power, ForceMode.Impulse);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Pistol is reloading");

        yield return new WaitForSeconds(ReloadTime);

        currentAmmo = magCapacity;
        isReloading = false;

        UpdateAmmoUI(); 
        Debug.Log("Pistol Reload complete");
    }

  
    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {magCapacity}";
        }
    }
}