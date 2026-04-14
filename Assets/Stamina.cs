using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField]private float stamina;
    private float staminaRegen = 0.250f;
    private float staminaDrain = 0.175f;
    private bool startRegen;
    private Sprint sprint;
    private PlayerMovement playerMovement;
    private bool jumpDrained;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        sprint = GetComponent<Sprint>();
        stamina = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (sprint.IsSprinting)
        {
            stamina -= staminaDrain;
            StopAllCoroutines();
           startRegen = false;
        }
        else if (!startRegen)
        {
            StartCoroutine(WaitForRegen());
        }

        stamina = Mathf.Clamp(stamina, 0f, 100f);
  
        if (playerMovement.JumpedCheck && !jumpDrained)
        {
           jumpDrained = true;
            stamina -= 15f;
        }

        if (!playerMovement.JumpedCheck && Stam >= 15f && !playerMovement.GroundedCheck)
        {
            jumpDrained = false;
        }
    }


    private IEnumerator WaitForRegen()
    {
        startRegen = true;
        Debug.Log("isnotsprinting");
        yield return new WaitForSeconds(1.5f);
        while (stamina < 100 && !sprint.IsSprinting)
        {
            stamina += staminaRegen;
            yield return null;
        }

        startRegen = false;

    }

    private IEnumerator Jumped()
    {
        yield return new();
        stamina -= 30f;
        StopCoroutine(Jumped());

    }
    
    
    public float Stam
    {
        get { return stamina; }
        private set { stamina = value; }
    }


}
