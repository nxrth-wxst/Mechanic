using UnityEngine;
using UnityEngine.InputSystem;

public class Sprint : MonoBehaviour
{
    private int runSpeed;
   [SerializeField] private bool isSprinting;
    private Controls controls;
    private Stamina stamina;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSprinting = false;
        stamina = GetComponent<Stamina>();
    }

   public bool IsSprinting
    {
        get { return isSprinting; }
        private set { isSprinting = value; } 
    
    }

    private void Update()
    {
        if (stamina.Stam == 0f)
        {
            isSprinting = false;
        }
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
