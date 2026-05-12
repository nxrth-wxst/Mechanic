using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    private const float meleeDamage = 2f;
    private bool isActive = false;
    public void SetActive(bool active)
    {
        isActive = active;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        ICollidable collidable = other.GetComponent<ICollidable>();
        if (collidable != null)
        {
            collidable.OnCollision(meleeDamage);
        }
    }
}
