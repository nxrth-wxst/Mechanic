using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField]private bool plrNearDoor;
    private bool plrOpenedDoor;
    private bool interacting;
   
    private Controls controls;

    private float doorCost = 150f;
    [SerializeField] private float timeToHold = 0.10f;

    private void Awake()
    {
        controls = new Controls();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            plrNearDoor = true;
        }
        else
        {
            plrNearDoor = false;
        }
    }

    private void Update()
    {
        if (plrNearDoor)
        {
            controls.Enable();
        }
        else
        {
            controls.Disable();
        }
    
        if (interacting)
        {
            timeToHold = Mathf.Clamp(timeToHold, 0, 0.10f);
            timeToHold -= 0.1f * Time.deltaTime;
        }
        else
        {
            timeToHold = 0.10f;
        }
    
        if (timeToHold <= 0f)
        {
            Debug.Log("doorbought");
        } 
    }



    private void OnEnable()
    {
        controls.Player.Interaction.started += OnInteractionStarted;
        controls.Player.Interaction.canceled += OnInteractionCanceled;
    }

    private void OnDisable()
    {
        controls.Player.Interaction.started -= OnInteractionStarted;
        controls.Player.Interaction.canceled -= OnInteractionCanceled;
    }

    private void OnInteractionStarted(InputAction.CallbackContext context)
    {
       interacting = true;
    }

    private void OnInteractionCanceled(InputAction.CallbackContext context)
    {
        interacting = false;
    }


}
