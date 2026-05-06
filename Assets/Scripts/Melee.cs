using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;


public class Melee : MonoBehaviour
{
    private Controls controls;
    [SerializeField] private GameObject melee;
    private bool Attack = true;
    [SerializeField] private float AttackCooldown = 4;

    void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Melee.started += MeleeStarted;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Melee.canceled -= MeleeStarted;
    }

    private void MeleeStarted(InputAction.CallbackContext context)
    {
        Attack = false;
        Animator anim = melee.GetComponent<Animator>();
        anim.SetTrigger("Attack");
    }
    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        Attack = true;
    }





}
