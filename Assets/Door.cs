using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private bool plrNearDoor;
    private bool plrOpenedDoor;
    private bool interacting;

    private Controls controls;
    private ScoreManager scoreManager;

    private float doorCost = 150f;
    [SerializeField] private float timeToHold = 0.10f;
    private Animator animator;

    private void Awake()
    {
        controls = new Controls();
        scoreManager = FindObjectsByType<ScoreManager>(FindObjectsSortMode.None)[0];
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            plrNearDoor = true;
        else
            plrNearDoor = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            plrNearDoor = false;
    }

    private void Update()
    {
        if (plrNearDoor)
            controls.Enable();
        else
            controls.Disable();

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
            if (scoreManager.GetMoney >= doorCost)
            {
                DoorBought();
            }
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

    private void DoorBought()
    {
        if (plrOpenedDoor) return;
        plrOpenedDoor = true;
        interacting = false;
        controls.Disable();
        scoreManager.SpendMoney(doorCost);
        animator.SetTrigger("Purchase");
    }
}
