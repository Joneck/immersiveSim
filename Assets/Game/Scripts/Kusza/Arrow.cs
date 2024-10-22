using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rb;
    public float ArrowForce;

    void Start()
    {
        transform.SetParent(null);
        rb.AddForce(transform.forward * ArrowForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.name);
        if(collision.gameObject.layer != 8)
        {
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
