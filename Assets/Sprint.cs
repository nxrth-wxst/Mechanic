using UnityEngine;
using UnityEngine.InputSystem;

public class Sprint : MonoBehaviour
{
    private int runSpeed;
    private float stamina;
    private bool isSprinting;
    private Controls controls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stamina = 100f;
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.Sprint.performed += Sprint_formed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Sprint_formed(InputAction.CallbackContext context)
    {
        Debug.Log("issprinting");
    }



}
