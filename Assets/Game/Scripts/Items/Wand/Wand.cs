using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public GameObject Arrow;
    public Transform ArrowPlace;
    public int magazine;
    public int maxMagazine;
    public bool CanShoot = true;

    void Update()
    {
        if (Input.GetMouseButton(0) && transform.parent != null && transform.parent.name == "Hand" && CanShoot)
        {
            magazine--;
            if (magazine == 0)
            {
                CanShoot = false;
                StartCoroutine(Relocad());
            }
            GameObject NewArrow = Instantiate(Arrow, gameObject.transform);
            NewArrow.transform.position = ArrowPlace.transform.position;
        }
    }

    IEnumerator Relocad()
    {
        yield return new WaitForSeconds(1f);
        magazine = maxMagazine;
        CanShoot = true;
    }
}
