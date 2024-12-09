using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeNumber : MonoBehaviour
{
    public int Number = 0;
    public int Modulo;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Number = Number+1 < Modulo ? Number+1 : 0;
            transform.GetChild(0).GetComponent<TextMeshPro>().text = Number.ToString();
            transform.parent.GetComponent<DoorCode>().CheckCode();
        }
    }
}
