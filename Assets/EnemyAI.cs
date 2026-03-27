using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    private UnityEngine.AI.NavMeshAgent basicEnemy;
    
    
    
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
