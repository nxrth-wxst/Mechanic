using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    private UnityEngine.AI.NavMeshAgent basicEnemy;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (basicEnemy.isOnNavMesh)
        {
            basicEnemy.SetDestination(PlayerMovement.Instance.transform.position);
        }
    }

}
