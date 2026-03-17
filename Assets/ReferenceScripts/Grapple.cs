using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [Header("Positions")]
    public Transform grappleStartPoint; // drag in inspector
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Camera playerCamera;
    
    
    [Header("Settings")]
    public KeyCode grappleInputKey;
    public float maxGrappleDistance;
    public LayerMask grappleLayer;
    public bool lookingatGrappleSurface;
    
    
    [Header("Physics")]
    public Rigidbody playerRb;
    public float grappleForce;
    public bool grappleEnabled;

    [Header("Visuals")]
    public LineRenderer line;

    [Header("Physics")]
    private Jump jumpScript;





    // Start is called before the first frame update
    void Start()
    {
        grappleEnabled = false;
        jumpScript = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        startPoint = grappleStartPoint.position;

        if (Input.GetKeyDown(grappleInputKey))
        {
            StartGrapple();
        }
        else if (Input.GetKey(grappleInputKey))
        {
            if (grappleEnabled)
            {
                ContinueGrapple();
            }
        }
        else if (Input.GetKeyUp(grappleInputKey))
        {
         
            EndGrapple();
        }

        RaycastHit hitInfo;

        if (Physics.Raycast(
            startPoint,
            playerCamera.transform.forward,
            out hitInfo,
            maxGrappleDistance,
            grappleLayer))
        {
           lookingatGrappleSurface = true;
        }
        else
        {
            lookingatGrappleSurface = false;
        }


    }

    void StartGrapple()
    {
        RaycastHit hitInfo;

        if(Physics.Raycast(
            startPoint, 
            playerCamera.transform.forward, 
            out hitInfo, 
            maxGrappleDistance, 
            grappleLayer))
        {
            grappleEnabled = true;
            line.enabled = true;
            endPoint = hitInfo.point;
            line.SetPosition(1, endPoint);
        }
    }

    void ContinueGrapple()
    {
        jumpScript.bhoptimerdecrease = false;
        playerRb.AddForce(
            (endPoint - startPoint).normalized * grappleForce);

        line.SetPosition(0, startPoint);
    
    }

    void EndGrapple()
    {
        if (grappleEnabled)
        {
            playerRb.AddForce(transform.forward * 1000);
        }
        
        grappleEnabled = false;
        line.enabled = false;
    }


}




