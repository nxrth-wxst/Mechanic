using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;
    public Rigidbody playerPhysics;
    [SerializeField] private float timer = 0f;

    private Controls controls;
    private Rigidbody rb;

    void Update()
    {

        if (timer > 0f)
        {

            speed = 8f;
            timer -= (Time.deltaTime * 1f);
            jump = 0.60f;


        }

        if (timer < 0f)
        {
            speed = 5f;
            jump = 0.15f;
        }

        // let's POLL!
        Vector2 walkInput = controls.Player.Walk.ReadValue<Vector2>();
        Vector3 walkVector = new Vector3(walkInput.x, 0, walkInput.y);
        transform.Translate(walkVector * Time.deltaTime * 10);

        //       if (Input.GetKey(KeyCode.Space))
        //      {
        //          playerPhysics.AddForce(Vector3.up * jump, ForceMode.Impulse);
        //      }


        //     float moveHorizontal = Input.GetAxis("Horizontal");
        //     float moveVertical = Input.GetAxis("Vertical");



        //      Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //       transform.Translate(movement * speed * Time.deltaTime);

    }

    void Start()
    {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Jump.performed += Jump_performed; // subscribing, not jumping here
        // anytime the jump action is performed, the Jump_performed method will get called automatically

        rb = GetComponent<Rigidbody>();
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        rb.AddForce(transform.up * 5, ForceMode.Impulse);
    }
}
