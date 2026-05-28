using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponEquip : MonoBehaviour
{
    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;
    [SerializeField] private GameObject gun3;

    private Controls controls;

    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.SwitchWeapon1.performed += SwitchWeapon1_formed;
        controls.Player.SwitchWeapon2.performed += SwitchWeapon2_formed;
        controls.Player.SwitchWeapon3.performed += SwitchWeapon3_formed;
    }

    void OnDisable()
    {
        controls.Player.SwitchWeapon1.performed -= SwitchWeapon1_formed;
        controls.Player.SwitchWeapon2.performed -= SwitchWeapon2_formed;
        controls.Player.SwitchWeapon3.performed -= SwitchWeapon3_formed;
        controls.Player.Disable();
    }

    private void Start()
    {
        SwitchWeapon1_formed(new InputAction.CallbackContext());
    }

    private void SwitchWeapon1_formed(InputAction.CallbackContext context)
    {
        if (gun1 != null) gun1.SetActive(true);
        if (gun2 != null) gun2.SetActive(false);
        if (gun3 != null) gun3.SetActive(false);
        Debug.Log("gun 1 active");
    }

    private void SwitchWeapon2_formed(InputAction.CallbackContext context)
    {
        if (gun2 != null) gun2.SetActive(true);
        if (gun1 != null) gun1.SetActive(false);
        if (gun3 != null) gun3.SetActive(false);
        Debug.Log("gun 2 active");
    }

    private void SwitchWeapon3_formed(InputAction.CallbackContext context)
    {
        if (gun3 != null) gun3.SetActive(true);
        if (gun1 != null) gun1.SetActive(false);
        if (gun2 != null) gun2.SetActive(false);
        Debug.Log("gun 3 active");
    }
}