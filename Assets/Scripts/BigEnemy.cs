using UnityEngine;

public class BigEnemy : MonoBehaviour, ICollidable
{
    [SerializeField]private float dummyHealth = 45f;

    private const float DummyHealth = 0f;

    void ICollidable.OnCollision(float damage)
    {
        Debug.Log("bigwashit");
        dummyHealth -= damage;
    }

    public void Update()
    {
        if (dummyHealth < DummyHealth)
        {
            Destroy(gameObject);
        }
    }



}
