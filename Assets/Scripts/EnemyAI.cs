using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private UnityEngine.AI.NavMeshAgent basicEnemy;
    private float updateTargetInterval = 0.2f; 
    private float timer;
    
    private void OnTriggerEnter(Collider other)
    {
        PColliable pCollidable = other.GetComponent<PColliable>();
        if (pCollidable != null)
        {
            pCollidable.PlayerCollision(this);
        }
    
    
    
    }
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basicEnemy = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (Barricade.Instance != null)
        {
            target = Barricade.Instance.getNavmeshTarget();
        }
        else Debug.LogWarning("No Barricade instance found!", this);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= updateTargetInterval)
        {
            basicEnemy.SetDestination(target.position);
            timer = 0f;
        }
    }



}
