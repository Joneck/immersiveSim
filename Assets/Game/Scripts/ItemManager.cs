using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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


    //Grabing Objects
    public KeyCode grabButton = KeyCode.E;
    Transform objectHeld = null;
    public Transform objectSocket;


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

        if(Input.GetKeyDown(grabButton))
        {
            if(objectHeld == null)
            {
                Debug.DrawRay(transform.position,transform.forward * 3, Color.red,3f);
                if(Physics.Raycast(transform.position, transform.forward, out hit, 2f,LayerMask.GetMask("Ground")))
                {
                    if(hit.collider.tag == "Grab")
                    {
                        GrabObject(hit.transform);
                    }
                    
                }
            }else{
                DropObject(objectHeld);
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
        HoldingItem = Items.Count > 0 ? Items[ActualSlot] : null;
        if(HoldingItem!= null)
        {
            HoldingItem.SetActive(true);
        }
    }

    void GrabObject(Transform thing) //Grabbing not usable objects
    {
        objectHeld = thing;
        thing.transform.position = objectSocket.position;
        thing.transform.SetParent(gameObject.transform.parent);
        Destroy(thing.GetComponent<Rigidbody>());
        thing.GetComponent<Collider>().enabled = false;
    }

    void DropObject(Transform thing) //Dropping not usable objects
    {
        objectHeld = null;
        thing.AddComponent<Rigidbody>();
        thing.GetComponent<Collider>().enabled = true;
        thing.transform.SetParent(null);
    }
}