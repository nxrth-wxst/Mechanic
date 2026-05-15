using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform barricadeTarget;
    private Transform playerTarget;
    private NavMeshAgent basicEnemy;
    private float updateTargetInterval = 0.2f;
    private float timer;
    private Animator animator;
    private Barricade nearestBarricade;
    private Coroutine replayCoroutine;
    private bool attackOccurred;
    [SerializeField] private bool passedThruBarricade;
    
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
        passedThruBarricade = false;
        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateTargetInterval)
        {
           
            {
                timer = 0f;
                UpdateNearestBarricade();

                if (barricadeTarget != null)
                    basicEnemy.SetDestination(barricadeTarget.position);
            }
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
        passedThruBarricade = value;




    }


    private void UpdateNearestBarricade()
    {
       
        Barricade[] allBarricades = FindObjectsByType<Barricade>(FindObjectsSortMode.None);

        if (allBarricades.Length == 0)
        {
            Debug.LogWarning("No Barricades found in scene!", this);
            return;
        }

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
            {
                barricadeTarget = playerTarget;
                Debug.Log("headingforplayer");
            }
            else
            {
                barricadeTarget = nearest.getNavmeshTarget();
            }
            
         
        }
    }
}