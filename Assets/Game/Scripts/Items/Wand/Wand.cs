using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public GameObject Arrow;
    public Transform ArrowPlace;
    public int magazine;
    public int maxMagazine;
    public bool CanReload = true;

    void Update()
    {
        if (transform.parent != null && transform.parent.name == "Hand")
        {
            if (Input.GetMouseButton(0) && magazine > 0)
            {
                magazine--;
                GameObject NewArrow = Instantiate(Arrow, gameObject.transform);
                NewArrow.transform.position = ArrowPlace.transform.position;
                if(magazine == 0)
                {
                    StartCoroutine(Relocad());
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && CanReload)
            {
                CanReload = false;
                StartCoroutine(Relocad());
            }
        }
    }

    void OnEnable()
    {
        CanReload = true;
    }

    IEnumerator Relocad()
    {
        yield return new WaitForSeconds(1f);
        magazine = maxMagazine;
        CanReload = true;
    }
}
