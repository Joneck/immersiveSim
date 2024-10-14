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

    float vertical;
    float horizontal;

    public bool isGrounded;

    public Animator anim;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                isGrounded = false;
                rb.AddForce(transform.up * jumpForce);
                if (vertical > 0)
                {
                    rb.AddForce(transform.forward * jumpForce / 2);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("Crouch");
        }

        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetTrigger("StandUp");
        }
    }

    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            actualSpeed = sprintSpeed;
        }
        else
        {
            actualSpeed = speed;
        }

        Vector3 velocity = moveDirection * actualSpeed * Time.fixedDeltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = false;
        }
    }
}
