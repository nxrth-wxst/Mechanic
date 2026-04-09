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
        isSprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        
 
    }

   public bool IsSprinting
    {
        get { return isSprinting; }
        private set { isSprinting = value; } 
    
    }

    
    
    
    private void Awake()
    {
        controls = new Controls();
    }



    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Sprint.started += OnSprintStarted;
        controls.Player.Sprint.canceled += OnSprintCanceled;
    }



    private void OnSprintStarted(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }

}
