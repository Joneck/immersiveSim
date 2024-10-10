using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float sprintSpeed;
    private float actualSpeed;
    public float jumpForce;

    public Transform orientation;
    private Vector3 moveDirection;
    Vector3 velocity;
    Rigidbody rb;

    public bool isGrounded;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            actualSpeed = sprintSpeed;
        }
        else
        {
            actualSpeed = speed;
        }


        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpForce);
                if(vertical > 0)
                {
                    rb.AddForce(transform.forward * jumpForce / 2);
                }
            }
        }
        if (isGrounded)
        {
            Vector3 velocity = moveDirection * actualSpeed * Time.fixedDeltaTime;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }
        else
        {
            Vector3 velocity = moveDirection * actualSpeed * Time.fixedDeltaTime;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = false;
        }
    }
}
