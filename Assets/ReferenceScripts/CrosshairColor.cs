using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrosshairColor : MonoBehaviour
{
    private Grapple grappleScript;
    public GameObject Player;
    public bool canGrappleSurface;
    public TextMeshProUGUI redCrosshair;
    
    
    // Start is called before the first frame update
    void Start()
    {
        grappleScript = Player.GetComponent<Grapple>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grappleScript.lookingatGrappleSurface)
        {
            canGrappleSurface = true;
        }
    
       if(grappleScript.lookingatGrappleSurface == false)
        {
            canGrappleSurface = false;
        }
        
        
        
        if (canGrappleSurface)
        {
            //redCrosshair.GetComponent<TextMeshProUGUI>().enabled = true;    
            redCrosshair.gameObject.SetActive(true);
        }
        if (canGrappleSurface == false)
        {
            // redCrosshair.GetComponent<TextMeshProUGUI>().enabled = false;
            redCrosshair.gameObject.SetActive(false);   
        }
    
    }
}
