using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    Camera cam;
    public LayerMask IgnoringLayer;
    public Transform Hand;

    public List<GameObject> Items = new List<GameObject>();

    public int ActualSlot;
    public GameObject HoldingItem = null;

    public float aaa;
    void Start()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
        aaa = Input.GetAxis("Mouse ScrollWheel");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = cam.ScreenToWorldPoint(mousePos);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ~IgnoringLayer))
        {
            if(hit.transform.gameObject.layer == 6 && Input.GetKeyDown(KeyCode.E))
            {
                GrabItem(hit.transform.gameObject);
            }
        }

        if(Items.Count > 0)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                ActualSlot = ActualSlot + 1 < Items.Count ? ActualSlot+=1 : ActualSlot = 0;
                HoldingItem.SetActive(false);
                HoldingItem = Items[ActualSlot];
                HoldingItem.SetActive(true);
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                ActualSlot = ActualSlot - 1 > -1 ? ActualSlot-=1 : ActualSlot = Items.Count-1;
                HoldingItem.SetActive(false);
                HoldingItem = Items[ActualSlot];
                HoldingItem.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.G))
            {
                DropItem();
            }
        }
    }

    void GrabItem(GameObject newItem)
    {
        newItem.SetActive(false);
        Destroy(newItem.GetComponent<Rigidbody>());
        newItem.transform.SetParent(Hand);
        newItem.transform.localPosition = Vector3.zero;
        Items.Add(newItem);
        newItem.transform.localEulerAngles = newItem.GetComponent<ItemStats>().BasicRotation;

        if(Items.Count == 1)
        {
            HoldingItem = newItem;
            newItem.SetActive(true);
        }
    }

    void DropItem()
    {
        HoldingItem.transform.SetParent(null);
        HoldingItem.AddComponent<Rigidbody>();
        HoldingItem.GetComponent<Rigidbody>().AddForce(transform.forward*5, ForceMode.Impulse);

        Items.RemoveAt(ActualSlot);
        ActualSlot = ActualSlot + 1 < Items.Count ? ActualSlot += 1 : ActualSlot = 0;
        Debug.Log(Items.Count);
        HoldingItem = Items.Count > 0 ? Items[ActualSlot] : null;
        if(HoldingItem!= null)
        {
            HoldingItem.SetActive(true);
        }
    }
}
