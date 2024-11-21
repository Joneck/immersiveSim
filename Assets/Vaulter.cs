using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaulter : MonoBehaviour
{
    CapsuleCollider vaultCheck;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        vaultCheck = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Checker();
    }

    void Checker()
    {
        Debug.DrawRay(transform.position, transform.forward * 0.45f, Color.magenta, 1f);

        // Use a direction and a small max distance #nic nie rozumiem
        Vector3 direction = transform.forward;
        float maxDistance = 0.1f; // Small distance to check for collisions

        if (Physics.SphereCast(transform.position, 0.5f, direction, out hit, maxDistance, LayerMask.GetMask("Ground")))
        {
            Debug.Log(hit.point + " from vaulter");
            Debug.Log("smthng");
        }
    }


    void OnDrawGizmos()
    {
        // Set the color for the Gizmos
        Gizmos.color = Color.green;

        // Draw the sphere at the object's position
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }
}
