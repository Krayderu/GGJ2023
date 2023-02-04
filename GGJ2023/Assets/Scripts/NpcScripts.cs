using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScripts : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Transform[] hoverPoints;

    public int currentWaypoint;
    public int tilePlaced;


    

    private void Update()
    {
        
        MoveTowards(wayPoints[1]);
        if(Vector3.Distance(transform.position, wayPoints[1].position) <= 1)
        {
            currentWaypoint = 1;
            HoverAround(wayPoints[1]);
        }
        if (currentWaypoint == 1 && tilePlaced >= 5)
        {
            MoveTowards(wayPoints[0]);
        }
    }

    private void MoveTowards(Transform pos)
    {
        Vector3 destination = pos.position;
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
    }

    private void HoverAround(Transform pos)
    {
      
    }
}
