using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform barricadeTarget;
    private Transform playerTarget;
    private NavMeshAgent basicEnemy;
    private float timer;
    private Animator animator;
    private Barricade nearestBarricade;
    private Coroutine replayCoroutine;
    private bool attackOccurred;
    [SerializeField] private bool passedThruBarricade;
    private bool isTraversingLink = false;

    private void OnTriggerEnter(Collider other)
    {
        PColliable pCollidable = other.GetComponent<PColliable>();
        if (pCollidable != null)
        {
            pCollidable.PlayerCollision(this);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        basicEnemy = GetComponent<NavMeshAgent>();
        basicEnemy.autoTraverseOffMeshLink = false;
        passedThruBarricade = false;
        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (basicEnemy.isOnOffMeshLink && !isTraversingLink)
        {
            StartCoroutine(TraverseLink());
            return;
        }

        if (isTraversingLink) return;

        timer += Time.deltaTime;

        float interval = passedThruBarricade ? 0.05f : 0.2f;

        if (timer >= interval)
        {
            timer = 0f;
            UpdateNearestBarricade();

            if (barricadeTarget != null)
                basicEnemy.SetDestination(barricadeTarget.position);
        }

        if (nearestBarricade != null && nearestBarricade.EmyNearBarricade)
        {
            animator.SetBool("BarricadeAttack", true);
            if (replayCoroutine == null)
            {
                replayCoroutine = StartCoroutine(ReplayAnim());
            }
        }
        else
        {
            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
                replayCoroutine = null;
            }
            animator.SetBool("BarricadeAttack", false);
            animator.SetBool("RefreshAttack", false);
        }
    }

    private IEnumerator TraverseLink()
    {
        isTraversingLink = true;

        OffMeshLinkData linkData = basicEnemy.currentOffMeshLinkData;
        Vector3 startPos = transform.position;
        Vector3 endPos = linkData.endPos + Vector3.up * basicEnemy.baseOffset;

        float traverseSpeed = 1.5f;
        float distance = Vector3.Distance(startPos, endPos);
        float duration = distance / traverseSpeed;
        float elapsed = 0f;

        animator.SetBool("WindowClimb", true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            transform.position = Vector3.Lerp(startPos, endPos, t);

            Vector3 direction = (endPos - transform.position).normalized;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);

            yield return null;
        }

        transform.position = endPos;
        animator.SetBool("WindowClimb", false);
        basicEnemy.CompleteOffMeshLink();
        isTraversingLink = false;
    }

    private void OnDestroy()
    {
        if (nearestBarricade != null)
            nearestBarricade.NotifyEnemyDead();
    }

    private void SubscribeToBarricade()
    {
        if (nearestBarricade != null)
            nearestBarricade.OnDamaged += () => attackOccurred = true;
    }

    private IEnumerator ReplayAnim()
    {
        attackOccurred = false;
        SubscribeToBarricade();

        while (nearestBarricade != null && nearestBarricade.EmyNearBarricade)
        {
            yield return new WaitUntil(() => attackOccurred ||
                                            nearestBarricade == null ||
                                            !nearestBarricade.EmyNearBarricade);

            if (nearestBarricade == null || !nearestBarricade.EmyNearBarricade) break;

            attackOccurred = false;

            animator.SetBool("BarricadeAttack", true);
            animator.SetBool("RefreshAttack", false);

            yield return new WaitForSeconds(0.5f);

            animator.SetBool("BarricadeAttack", false);
            animator.SetBool("RefreshAttack", true);
        }

        animator.SetBool("BarricadeAttack", false);
        animator.SetBool("RefreshAttack", false);
        replayCoroutine = null;
    }

    public void setPassthru(bool value)
    {
        if (passedThruBarricade == value) return;
        passedThruBarricade = value;
    }

    private void UpdateNearestBarricade()
    {
        Barricade[] allBarricades = FindObjectsByType<Barricade>(FindObjectsSortMode.None);
        if (allBarricades.Length == 0) return;

        Barricade nearest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Barricade barricade in allBarricades)
        {
            float distance = Vector3.Distance(transform.position, barricade.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = barricade;
            }
        }

        if (nearest != null)
        {
            nearestBarricade = nearest;

            if (passedThruBarricade)
                barricadeTarget = playerTarget;
            else
                barricadeTarget = nearest.getNavmeshTarget();
        }
    }
}