using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    Rigidbody rb;

    bool grapple = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cam.transform.position,cam.transform.forward,Color.cyan,0);

        if(Input.GetMouseButtonDown(1))
        {
            grapple = true;
        }
    }

    void FixedUpdate()
    {
        if(grapple)
        {
            grapple = false;
            rb.AddForce(cam.transform.forward * 45, ForceMode.Impulse);
            Debug.Log("grappled");
        }
        
    }
}
