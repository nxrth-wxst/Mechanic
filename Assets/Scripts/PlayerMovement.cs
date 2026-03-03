using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;
   // public Rigidbody playerPhysics;


    void Update()
    {

  //      if (Input.GetKey(KeyCode.Space))
  //      {
  //          playerPhysics.AddForce(transform.up * jump);
//        }


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(movement * speed * Time.deltaTime);
    }



}
