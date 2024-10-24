using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject player;
    private GameObject grapplingGun;

    public float Force = 10f;
    public float GrapplingForce = 5f;

    void Start()
    {
        player = GameObject.Find("Player");
        grapplingGun = GameObject.Find("GrapplingGun");
        transform.SetParent(null);
        rb.AddForce(transform.up * Force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Destroy(rb);
            
            player.GetComponent<Rigidbody>().AddForce((transform.position - player.transform.position).normalized * GrapplingForce, ForceMode.Impulse);
            grapplingGun.GetComponent<Grappling>().StartSuperSpeed();
            GetComponent<CapsuleCollider>().isTrigger = true;
            Destroy(this);

            Debug.Log("Adding Extra Force" + collision.gameObject.name);
        }
    }
}
