using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using JetBrains.Annotations;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    Rigidbody rb;
    PlayerMovement movement;
    public GameObject Hook;
    bool grapple = false;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        movement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cam.transform.position,cam.transform.forward,Color.cyan,0);

        if(Input.GetMouseButtonDown(1) && transform.parent.name == "ItemManager")
        {
            GameObject NewHook = Instantiate(Hook, gameObject.transform);
            NewHook.transform.position = transform.position;
        }
    }

    public void StartSuperSpeed()
    {
        StartCoroutine("SuperSpeed");
    }


    IEnumerator SuperSpeed()
    {
        movement.SwitchSpeedLockOFF();
        yield return new WaitForSeconds(0.5f);
        movement.SwitchSpeedLockON();
    }

    void FixedUpdate()
    {
        if(grapple)
        {
            grapple = false;
            Debug.Log("grappled");
        }
        
    }
}
