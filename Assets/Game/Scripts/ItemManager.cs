using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Camera cam;
    public LayerMask IgnoringLayer;
    public Transform Hand;

    public List<GameObject> items = new List<GameObject>();

    //private float fire;

    
    void Start()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = cam.ScreenToWorldPoint(mousePos);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ~IgnoringLayer))
        {
            if(hit.transform.gameObject.layer == 6 && Input.GetKeyDown(KeyCode.E))
            {
                GameObject newItem = hit.transform.gameObject;
                Destroy(newItem.GetComponent<Rigidbody>());
                newItem.transform.SetParent(Hand);
                newItem.transform.localPosition = Vector3.zero;
                items.Add(newItem);
            }
        }
    }
}
