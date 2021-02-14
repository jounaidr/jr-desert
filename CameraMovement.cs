using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float MOUSE_SENSITIVITY = 400f;

    private float yRotation = 0f;

    public Transform playerObject;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide cursor
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MOUSE_SENSITIVITY * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MOUSE_SENSITIVITY * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f); //Lock y rotation to 180 degrees

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f); // Rotate camera in y direction
        playerObject.Rotate(Vector3.up * mouseX); // Rotate player object with camera in x direction
    }
}
