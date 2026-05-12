using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Melee : MonoBehaviour
{
    private Controls controls;
    [SerializeField] private GameObject melee;
    private bool canAttack = true;
    private MeleeHit meleeHitbox;

    private const float AttackCooldown = 1f;
    private const float AttackWindowCooldown = 0.3f;

    void Awake()
    {
        controls = new Controls();
        meleeHitbox = melee.GetComponent<MeleeHit>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Melee.started += MeleeStarted;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Melee.started -= MeleeStarted;
    }

    private void MeleeStarted(InputAction.CallbackContext context)
    {
        if (!canAttack) return;

        canAttack = false;
        Animator anim = melee.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(AttackWindow());
        StartCoroutine(ResetCooldown());
    }
    private IEnumerator AttackWindow()
    {
        meleeHitbox.SetActive(true);
        yield return new WaitForSeconds(AttackWindowCooldown);
        meleeHitbox.SetActive(false);
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
}