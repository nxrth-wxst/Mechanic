using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;
    public Rigidbody playerPhysics;
    [SerializeField] private float timer = 0f;

    void Update()
    {

        if (timer > 0f)
        {

            speed = 8f;
            timer -= (Time.deltaTime * 1f);
            jump = 0.60f;


        }

        if (timer < 0f)
        {
            speed = 5f;
            jump = 0.15f;
        }



        if (Input.GetKey(KeyCode.Space))
        {
            playerPhysics.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(movement * speed * Time.deltaTime);
        Debug.Log(timer);
    }

    public void SpeedBoost()
    {
        timer = 3f;
        timer -= (Time.deltaTime * 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollidable collidable = other.GetComponent<ICollidable>();

        if (collidable != null)
        {
            SpeedBoost();
        }

    }
}
