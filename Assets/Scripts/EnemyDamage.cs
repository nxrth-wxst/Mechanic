using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PColliable pCollidable = other.GetComponent<PColliable>();
        if (pCollidable != null)
        {
            pCollidable.PlayerCollision(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
