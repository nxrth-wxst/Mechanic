using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Player;

    public float xSensitivity;
    public float ySensitivity;
    public float xRotation;
    public float yRotation;
    private const float Cameraheight = -90;
    private const float Maxcamera = 90;
    private const float Mincamera = 0.2f;
    private const float PlayerRoate = 0;
    private const float Transformrotation = 0;
    private const float PlayerRotation = 0;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void LateUpdate()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, Cameraheight, Maxcamera);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, Transformrotation);
        Player.rotation = Quaternion.Euler(PlayerRotation, yRotation, PlayerRoate);

        transform.position = new Vector3(Player.position.x, Player.position.y + Mincamera, Player.position.z);
    }
}
