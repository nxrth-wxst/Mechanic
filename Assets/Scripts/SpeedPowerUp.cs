using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, ICollidable
{
    void ICollidable.OnCollision(PlayerMovement player)
    {
        Debug.Log("Was Hit");
    }
}

