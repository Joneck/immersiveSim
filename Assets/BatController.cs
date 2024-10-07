using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    Animator anim;
    bool fireDown = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Fire1") == 1.0f) //wait until animation ends to get input again
        {
            if(!fireDown)
                fireDown = true;
            
            else
                Debug.Log("fired");
                anim.SetTrigger("Swing");
                fireDown = false;
        }
    }
}
