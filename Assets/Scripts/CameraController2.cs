using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    /*
     * Much of this script is copied from:
     * https://www.youtube.com/watch?v=f473C43s8nE
     */

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float xSensitivity;

    [SerializeField]
    private float ySensitivity;

    private float xRotation;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        // lock the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // make the cursor invisible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);

        transform.position = playerTransform.position + playerTransform.forward;
    }
}
