using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    private float fire;

    // Start is called before the first frame update
    void Start()
    {
        items.Add(GameObject.Find("Stick"));
    }

    // Update is called once per frame
    void Update()
    { 
        
    }
}
