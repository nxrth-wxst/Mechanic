using UnityEngine;
using UnityEngine.InputSystem;

public class GunBob : MonoBehaviour
{
    [SerializeField] private Animator gunAnim;

    private Controls controls;

    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Walk.started += Walk_started;
        controls.Player.Walk.canceled += Walk_canceled;
    }

    void OnDisable()
    {
        controls.Player.Walk.started -= Walk_started;
        controls.Player.Walk.canceled -= Walk_canceled;
        controls.Player.Disable();
    }

    private void Walk_started(InputAction.CallbackContext context)
    {
        if (gunAnim != null)
        {
            gunAnim.SetTrigger("move");
            gunAnim.ResetTrigger("stop");
        }
    }

    private void Walk_canceled(InputAction.CallbackContext context)
    {
        if (gunAnim != null)
        {
            gunAnim.SetTrigger("move");
            gunAnim.ResetTrigger("stop");
        }
    }
}