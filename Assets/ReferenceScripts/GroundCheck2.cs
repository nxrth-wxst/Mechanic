using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck2 : MonoBehaviour
{
    [Header("Bhop")]
    public float BHopWindow;
    public bool bHopAvailable;
    
    public bool bhopspeedavail;
    private Jump jumpScript;
    public GameObject Player;
    
    [Header("Ground")]
    public bool isGrounded;
    public float checkDistance;
    public LayerMask layermask;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        bHopAvailable = false;
        bhopspeedavail = false;
        jumpScript = Player.GetComponent<Jump>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, checkDistance, layermask))
        {
          if(!isGrounded)
            {
                isGrounded =true;
                
                StartCoroutine(BHopTimer());
            }
        }
        else
        {


            isGrounded = false;
        }
    }

    private IEnumerator BHopTimer()
    {
        bHopAvailable = true;
        yield return new WaitForSeconds(BHopWindow);
        bHopAvailable = false;
        
    }
}
