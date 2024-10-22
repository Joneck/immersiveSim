using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatController : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && transform.parent.name == "Hand")
        {
            Debug.Log("fired");
            anim.SetTrigger("Swing");
        }
    }


}
