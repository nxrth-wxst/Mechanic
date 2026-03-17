using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    
    public float acceleration;

    private GroundCheck2 GroundCheck;
    public float maxSpeed;
    public float maxbhopSpeed;
    private Rigidbody playerRb;
    private Jump jumpScript;
   

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        
        jumpScript = GetComponentInChildren<Jump>();
        GroundCheck = GetComponentInChildren<GroundCheck2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forceDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            forceDirection += Vector3.forward;
           
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forceDirection += Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            forceDirection += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            forceDirection += Vector3.left;
        }

        forceDirection = forceDirection.normalized;

        playerRb.AddRelativeForce(forceDirection * acceleration);

        if (playerRb.linearVelocity.magnitude > maxSpeed)
        {
            playerRb.linearVelocity = playerRb.linearVelocity.normalized * maxSpeed;
        }

        if (GroundCheck.bhopspeedavail)
        {
            maxSpeed = 15f;

        }
        else
        {

            maxSpeed = 8f;

        }
    }
}
