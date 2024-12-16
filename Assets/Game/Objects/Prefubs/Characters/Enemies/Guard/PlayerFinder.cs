using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{

    //Components
    Rigidbody rb;

    public Transform Player;
    Vector3 playerPos;
    Vector3 dirToPlayer;

    private Quaternion _lookRotation;
    private float RotationSpeed = 3f;
    
    Color lineColor = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPos  = Player.position;
        Debug.Log(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos  = Player.position;

        dirToPlayer = (playerPos - transform.position).normalized;
        // inne kąty w płaszczyźne inne w pionie 
        // kiedy wykryje gracza niech się do niego odwraca i porusza w jego kierunku
        // obraca się -> jeszcze poruszanie
        
        float Angle = 45f;
        Vector3 rotatedVector1 = Quaternion.Euler(0, Angle, 0) * transform.forward;
        Vector3 rotatedVector2 = Quaternion.Euler(0, -Angle, 0) * transform.forward;

        Debug.DrawRay(transform.position, rotatedVector1, Color.yellow);
        Debug.DrawRay(transform.position, rotatedVector2, Color.yellow);
        Debug.DrawLine(transform.position,playerPos, lineColor);

        

        if(dirToPlayer.magnitude < 10)
        {
            if(Vector3.Angle(transform.forward, dirToPlayer) < 45)
            {
                lineColor = Color.red;
                _lookRotation = Quaternion.LookRotation(new Vector3(dirToPlayer.x,0,dirToPlayer.z).normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

                rb.velocity = new Vector3(dirToPlayer.x, 0, dirToPlayer.z).normalized;
                Debug.Log(_lookRotation.eulerAngles);
            }else{
                lineColor = Color.green;
                rb.velocity = Vector3.zero;
            }
        }else{
            rb.velocity = Vector3.zero;
            lineColor = Color.green;
        }
            
    }
}
