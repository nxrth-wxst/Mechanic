using UnityEngine;

public class Assault : MonoBehaviour
{
    public GameObject Bullet;
    public float launchVelocity = 1f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(Bullet, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0, launchVelocity, 0));
          
        }
    }


}
