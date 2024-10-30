using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rb;
    public float ArrowForce;

    public int FriendlyLayer1;
    public int FriendlyLayer2;

    public float TimeToDestroy;

    void Start()
    {
        transform.SetParent(null);
        rb.AddForce(transform.forward * ArrowForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.name);
        if(collision.gameObject.layer != FriendlyLayer1 && collision.gameObject.layer != FriendlyLayer2)
        {
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(gameObject);
    }
}
