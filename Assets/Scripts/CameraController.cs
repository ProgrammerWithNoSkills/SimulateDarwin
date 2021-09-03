using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Look Sensitivity")]
    public float sensX;
    public float sensY;

    [Header("Clamping")]
    public float minY = -90f;
    public float maxY = 90f;

    [Header("Spectator")]
    public float moveSpeed = 10f;

    private float rotX;
    private float rotY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void LateUpdate()
    {
        rotX += Input.GetAxis("Mouse X") * sensX;
        rotY += Input.GetAxis("Mouse Y") * sensY;

        rotY = Mathf.Clamp(rotY, minY, maxY);

        transform.rotation = Quaternion.Euler(-rotY, rotX, 0);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0;

        if (Input.GetKey(KeyCode.E))
            y = 1 * moveSpeed * 0.2f;
        else if (Input.GetKey(KeyCode.Q))
            y = -1 * moveSpeed * 0.2f;

        Vector3 dir = transform.right * x + transform.up * y + transform.forward * z;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
