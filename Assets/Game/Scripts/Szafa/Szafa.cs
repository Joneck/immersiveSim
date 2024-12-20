using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Szafa : MonoBehaviour
{
    private bool isUsed;

    public GameObject Player;
    private PlayerMovement playerMovement;

    public Transform PlayerOutCabinet;
    public Transform PlayerInCabinet;


    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.F) && isUsed == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            isUsed = true;
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<Rigidbody>().isKinematic = true;
            playerMovement = Player.GetComponent<PlayerMovement>();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraHandler>().yRotation = PlayerInCabinet.transform.eulerAngles.y;
            playerMovement.CanMove = false;
            Player.transform.position = PlayerInCabinet.position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isUsed == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            isUsed = false;
            Player.transform.position = PlayerOutCabinet.position;
            Player.GetComponent<Rigidbody>().isKinematic = false;
            playerMovement.CanMove = true;
        }
    }
}
