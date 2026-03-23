using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]private Transform playerLocation;
    private UnityEngine.AI.NavMeshAgent basicEnemy;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        basicEnemy.destination = playerLocation.position;
    }
}
