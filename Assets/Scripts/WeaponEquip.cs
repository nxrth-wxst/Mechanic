using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponEquip : MonoBehaviour
{
    [SerializeField]private GameObject gun1;
    [SerializeField]private GameObject gun2;
    [SerializeField]private GameObject gun3;
    private Controls controls;
    private bool Gun1Equipped;
    private bool Gun2Equipped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.SwitchWeapon1.performed += SwitchWeapon1_formed;
        controls.Player.SwitchWeapon2.performed += SwitchWeapon2_formed;


    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void SwitchWeapon1_formed(InputAction.CallbackContext context)
    {
        gun1.SetActive(true);
        Gun1Equipped = true;
        Gun2Equipped = false;
        gun2.SetActive(false);
    }

    private void SwitchWeapon2_formed(InputAction.CallbackContext context)
    {
        gun2.SetActive(true);
        Gun2Equipped = true;
        Gun1Equipped = false;
        gun1.SetActive(false);
    }

  
}
