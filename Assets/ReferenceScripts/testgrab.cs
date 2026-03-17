using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class testgrab : MonoBehaviour
{
    public GameObject Player;
    private Grapple Grapple;
    
    // Start is called before the first frame update
    void Start()
    {
        Grapple = Player.GetComponent<Grapple>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grapple.grappleEnabled)
        {
            
        }
    }
}
