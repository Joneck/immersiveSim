using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
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

    public CapsuleCollider vaultCheck;
    private bool speedLocked = true;
    Vector3 velocity;
    Rigidbody rb;
    Transform top;

    float vertical;
    float horizontal;

    RaycastHit hit;

    public bool isGrounded;
    bool wantToStandUp = false;

    public Animator anim;

    public float normalAccelerationForce = 2;
    public float sprintAccelerationForce = 3;
    private float currentAccelerationForce = 2;

  
    public float normalMaxMovementSpeed = 3;
    public float sprintMaxMovementSpeed = 4;
    private float currentMaxMovementSpeed = 3;

    public bool CanMove;

    private void Start()
    {
        CanMove = true;
        rb = GetComponent<Rigidbody>();
        top = GameObject.Find("Top").GetComponent<Transform>();
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            wantToStandUp = false;
            anim.SetTrigger("Crouch");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))//
        {
            wantToStandUp = false;

            if (Physics.CheckCapsule(top.position, top.position + Vector3.up * 0.6f, 0.8f, LayerMask.GetMask("Ground")))
            {
                Debug.Log("Can't stand up");
                wantToStandUp = true;

            }
            else
            {
                print(hit.collider);
                anim.SetTrigger("StandUp");
            }
        }


        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentAccelerationForce = sprintAccelerationForce;
            currentMaxMovementSpeed = sprintMaxMovementSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentAccelerationForce = normalAccelerationForce;
            currentMaxMovementSpeed = normalMaxMovementSpeed;
        }

        if (wantToStandUp)
            if (!Physics.CheckCapsule(top.position, top.position + Vector3.up * 0.6f, 0.8f, LayerMask.GetMask("Ground")))
            {
                anim.SetTrigger("StandUp");
                wantToStandUp = false;
            }
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            Movement();
            SpeedControl();
        }
    }

    private void Movement()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        //print(moveDirection);
        Vector3 velocity = moveDirection * currentAccelerationForce * Time.fixedDeltaTime * 1600;
        
        rb.AddForce(velocity,ForceMode.Force);
    }


    public void SwitchSpeedLockON()
    {
        speedLocked = true;
    }

    public void SwitchSpeedLockOFF()
    {
        speedLocked = false;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0,rb.velocity.z);
        
        if(speedLocked)
        {
            if(flatVel.magnitude > currentMaxMovementSpeed)
            {
            Vector3 limitedVel = flatVel.normalized * currentMaxMovementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        
    }

    private void OnTriggerStay(Collider collision)
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
