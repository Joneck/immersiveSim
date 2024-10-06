using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float SensX;
    public float SensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public int State;

    private void Update()
    {
        if (State == 0)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * SensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * SensY;

            yRotation += mouseX;
            xRotation += mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(-xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
}
