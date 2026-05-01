using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent basicEnemy;
    private float updateTargetInterval = 0.2f;
    private float timer;
    private Animator animator;
    private Barricade nearestBarricade; 

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
        UpdateNearestBarricade();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateTargetInterval)
        {
            timer = 0f;
            UpdateNearestBarricade();

            if (target != null)
                basicEnemy.SetDestination(target.position);
        }

        
        if (nearestBarricade != null && nearestBarricade.EmyNearBarricade)
        {
            animator.SetBool("BarricadeAttack", true);
        }
        else
        {
            animator.SetBool("BarricadeAttack", false); 
        }
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
            target = nearest.getNavmeshTarget();
        }
    }
}