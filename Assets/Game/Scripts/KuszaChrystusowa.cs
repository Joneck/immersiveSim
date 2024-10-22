using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuszaChrystusowa : MonoBehaviour
{
    public GameObject Arrow;
    public Transform ArrowPlace;

    void Update()
    {
        if (Input.GetMouseButton(0) && transform.parent.name == "Hand")
        {
            GameObject NewArrow = Instantiate(Arrow, ArrowPlace);
            NewArrow.transform.position = ArrowPlace.transform.position;
        }
    }

}
