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

    public float accelerationForce = 10;
    public float maxMovementSpeed = 50;
    public float maxSpeed = 7;
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

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        /*

        

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
        rb.velocity = velocity;*/

        Movement();
        SpeedControl();
    }

    private void Movement()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        //print(moveDirection);
        Vector3 velocity = moveDirection * accelerationForce * Time.fixedDeltaTime * 1600;
        
        rb.AddForce(velocity,ForceMode.Force);
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0,rb.velocity.z);
        
        if(flatVel.magnitude > maxMovementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxMovementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
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
