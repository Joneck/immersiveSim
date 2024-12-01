using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{

    public Transform Player;
    Vector3 playerPos;
    Color lineColor = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        playerPos  = Player.position;
        Debug.Log(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos  = Player.position;

        // kąt pomiędzy transform.forward i transform.position - playerPos ma być mniejszy niż cośtam
        // ale może tylko na płaszczyźnie X-Z 
        // kiedy wykryje gracza niech się do niego odwraca i porusza w jego kierunku
        
        float Angle = 45f;
        Vector3 rotatedVector1 = Quaternion.Euler(0, Angle, 0) * transform.forward;
        Vector3 rotatedVector2 = Quaternion.Euler(0, -Angle, 0) * transform.forward;

        Debug.DrawRay(transform.position, rotatedVector1, Color.yellow);
        Debug.DrawRay(transform.position, rotatedVector2, Color.yellow);
        Debug.DrawLine(transform.position,playerPos, lineColor);

        if(Vector3.Angle(transform.forward, playerPos - transform.position) < 45)
        {
            lineColor = Color.red;
        }else{
            lineColor = Color.green;
        }
    }
}
