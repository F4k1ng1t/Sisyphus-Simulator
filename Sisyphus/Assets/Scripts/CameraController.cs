using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Look Sensitivity")]
    public float sensX;
    public float sensY;
    [Header("Clamping")]
    public float minY;
    public float maxY;
    [Header("Spectator")]
    public float spectatorMoveSpeed;
    private float rotX;
    private float rotY;
    private bool isSpectator;
    void Start()
    {
        // lock the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        // get the mouse movement inputs
        rotX += Input.GetAxis("Mouse X") * sensX;
        rotY += Input.GetAxis("Mouse Y") * sensY;
        // clamp the vertical rotation
        rotY = Mathf.Clamp(rotY, minY, maxY);

        
        // rotate the camera vertically
        transform.localRotation = Quaternion.Euler(rotY, 0,0);
        // rotate the player horizontally
        transform.parent.rotation = Quaternion.Euler(transform.rotation.x, rotX, 0);
        
    }
    public void SetAsSpectator()
    {
        isSpectator = true;
        transform.parent = null;
    }
}
