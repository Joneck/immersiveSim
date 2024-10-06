using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;

    public float GroundDrag;
    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    Rigidbody rb;

    public float gravityScale;
    public static float globalGravity = -9.81f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpForce * Vector3.up);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed*1.4f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed/1.4f;
        }
    }

    public void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveSpeed * 10f * moveDirection.normalized, ForceMode.Force);

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
