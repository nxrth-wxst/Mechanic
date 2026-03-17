using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private PlayerMovement2 movementscript;
    private GroundCheck2 GroundCheck;
    [SerializeField]
    private KeyCode jumpKeyCode;
    private Rigidbody rb;
    [SerializeField]
    private float jumpForce;
    public float bhoptimer;
    public bool bhoptimerdecrease;
    // Start is called before the first frame update
    void Start()
    {
       GroundCheck = GetComponentInChildren<GroundCheck2>(); 
       rb = GetComponent<Rigidbody>();    
       movementscript = GetComponentInChildren<PlayerMovement2>();
        bhoptimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpKeyCode) && GroundCheck.isGrounded)
        {

            if (GroundCheck.bHopAvailable)
            {
                movementscript.acceleration += 2f;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                GroundCheck.bhopspeedavail = true;
                bhoptimer = 1.5f;
                StartCoroutine(BHopSpeedDecrease());
                 //rb.AddForce(Vector3.up * rb.velocity.magnitude * jumpForce, ForceMode.Impulse);

            }
           
            
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
        
        }

        if (bhoptimerdecrease)
        {
            if (bhoptimer > 0f)
            {
                if (GroundCheck.isGrounded)
                {
                    bhoptimer -= (1 * Time.deltaTime);
                }
            }
        }
        if (bhoptimer < 0f)
        {
            GroundCheck.bhopspeedavail = false;
        }
    }


    private IEnumerator BHopSpeedDecrease()
    {
        yield return new WaitForSeconds(2);
        movementscript.acceleration -= 2f;
        bhoptimerdecrease = true;
    
    }



}
