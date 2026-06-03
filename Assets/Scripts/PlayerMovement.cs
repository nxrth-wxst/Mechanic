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
    private int maxSprintSpeed = 7;

    [Header("MovementSettings")]
    [SerializeField] private float baseSpeed = 5f;
    private float jump = 5f;
    [SerializeField] private Rigidbody playerPhysics;
    private float jumpTimer = 0f;

    [SerializeField] private Animator gunAnim;

    [Header("CoreComponents")]
    public static PlayerMovement Instance { get; private set; }
    private Controls controls;
    private Rigidbody rb;
    private Stamina stamina;

    [Header("JumpSettings")]
    private bool isGrounded;
    private float checkDistance;
    [SerializeField] private LayerMask jumpableLayer;


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
            controls.Player.Jump.performed -= Jump_performed;  // subscribing, not jumping here
                                                               // anytime the jump action is performed, the Jump_performed method will get called automatically

            controls.Player.Disable();
        }
    }

    void Update()
    {

        jumpTimer = Mathf.Clamp(jumpTimer, 0f, 12f);




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


        if (jumpTimer > 0f)
        {
            StartCoroutine(Jumped());
            jump = 0.60f;
            jumpTimer -= (Time.deltaTime * 1f);
        }
        if (jumpTimer < 0f)
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

        if (isGrounded == true)
        {
            if (stamina.Stam >= 30f)
            {
                if (jumpTimer < 0.1f)
                {
                    rb.AddForce(transform.up * 3.5f, ForceMode.Impulse);
                    isGrounded = false;
                    jumpTimer += 1.65f;
                }
            }
        }
    }


    public bool GroundedCheck
    {
        get { return isGrounded; }
        private set { isGrounded = value; }
    }

    public float JumpTimer
    {
        get { return jumpTimer; }
        private set { jumpTimer = value; }
    }



    void Awake()
    {
        Instance = this;
        controls = new Controls();
    }


}