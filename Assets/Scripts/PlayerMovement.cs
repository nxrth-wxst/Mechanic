using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("SprintSettings")]
    private Sprint sprint;
    private float accelDecel = 0.025f;
    private int maxSprintSpeed = 9;

    [Header("MovementSettings")]
    [SerializeField] private float baseSpeed = 5f;
    private float jump = 5f;
    [SerializeField] private Rigidbody playerPhysics;
    [SerializeField] private float timer = 0f;


    [Header("CoreComponents")]
    public static PlayerMovement Instance { get; private set; }
    private Controls controls;
    private Rigidbody rb;
    private Stamina stamina;
    
    [Header("JumpSettings")]
    [SerializeField] private bool isGrounded;
    private float checkDistance;
    [SerializeField] private bool jumped;
    [SerializeField] private LayerMask jumpableLayer;
    private float checkJumpTime = 0.50f;
  
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
        timer = Mathf.Clamp(timer, 0f, 12f);

        if (timer < checkJumpTime)
        {
            jumped = false;
        }
        
        
        if (sprint.IsSprinting)
        {
            
                if (baseSpeed < maxSprintSpeed)
                {
                    baseSpeed += accelDecel;
                }

            
        }
        else
        {
            if (baseSpeed > 5)
            {
                baseSpeed -= accelDecel;
            }
        } 
        
        
        if (timer > 0f)
        {
            StartCoroutine(Jumped());
            jump = 0.60f;
            timer -= (Time.deltaTime * 1f);
        }
        if (timer < 0f)
        {
            if (baseSpeed > 5)
            {
            baseSpeed -= accelDecel;
            }
            jump = 0.15f;
        }

     
        Vector2 walkInput = controls.Player.Walk.ReadValue<Vector2>();
        Vector3 walkVector = new Vector3(walkInput.x, 0, walkInput.y);
        transform.Translate(walkVector * Time.deltaTime * baseSpeed);

        
        if (Physics.Raycast(transform.position, Vector3.down, checkDistance, jumpableLayer))
        {
            isGrounded = true;
            
        }
        else
        {
            isGrounded = false;
        }
        
        
    
    
    
    }

    private IEnumerator Jumped()
    {
       
        baseSpeed += 0.0025f;
        yield return new WaitForSeconds(0.5f);
        if (baseSpeed > 5)
        {
            if (sprint.IsSprinting)
            {
                StopCoroutine(Jumped());
            }
            else
            {
                if (baseSpeed > 5)
                {
                    
                    baseSpeed -= accelDecel;
                }
            }
        }
    }
    
    
    
    
    
    void Start()
    {
      
        isGrounded = false;
        rb = GetComponent<Rigidbody>();
        checkDistance = 1f;
        sprint = GetComponent<Sprint>();
        stamina = GetComponent<Stamina>();
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        jumped = true;
        if (isGrounded == true)
        {
            if (stamina.Stam >= 30f) 
            {
                if (timer < 0.1f)
                {
                    rb.AddForce(transform.up * 5, ForceMode.Impulse);
                    isGrounded = false;
                    timer += 1.65f;
                }
            }
        }
    }

    public bool JumpedCheck
    {
        get { return jumped; }
        private set { jumped = value; }
    }
    
    public bool GroundedCheck
    {
        get { return isGrounded; }
        private set { isGrounded = value; }
    }
    
    
    
    void Awake()
    {
        Instance = this;
        controls = new Controls();
    }


}
