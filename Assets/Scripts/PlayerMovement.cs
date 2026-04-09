using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private Sprint Sprint;
    private float speed = 5f;
    private float jump = 5f;
    [SerializeField] private Rigidbody playerPhysics;
    [SerializeField] private float timer = 0f;

    public static PlayerMovement Instance { get; private set; }

    private Controls controls;
    private Rigidbody rb;


    private bool isGrounded;
    private float CheckDistance;
    [SerializeField] private LayerMask JumpableLayer;

  
    void OnEnable()
    {
        if (controls != null)
        {
            controls.Player.Enable();
            controls.Player.Jump.performed += Jump_performed;
        }
    }

   
    void OnDisable()
    {
        if (controls != null)
        {
            controls.Player.Jump.performed -= Jump_performed;
            controls.Player.Disable();
        }
    }

    void Update()
    {
        
        
        
        
        
        if (timer > 0f)
        {
            speed = 8f;
            jump = 0.60f;
            timer -= (Time.deltaTime * 1f);
        }
        if (timer < 0f)
        {
            speed = 5f;
            jump = 0.15f;
        }

     
        Vector2 walkInput = controls.Player.Walk.ReadValue<Vector2>();
        Vector3 walkVector = new Vector3(walkInput.x, 0, walkInput.y);
        transform.Translate(walkVector * Time.deltaTime * speed);

        isGrounded = false;
        if (Physics.Raycast(transform.position, Vector3.down, CheckDistance, JumpableLayer))
        {
            isGrounded = true;
        }
    
    }

    void Start()
    {
      
        isGrounded = false;
        rb = GetComponent<Rigidbody>();
        CheckDistance = 1f;
        Sprint = GetComponent<Sprint>();
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {

        if (isGrounded == true)
        {
            if (timer < 0.1f)
            {
                rb.AddForce(transform.up * 5, ForceMode.Impulse);
                timer += 1.65f;
            }
        }
    }

    void Awake()
    {
        Instance = this;
        controls = new Controls();
    }
}
