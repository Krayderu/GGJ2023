using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScripts : MonoBehaviour
{
    [SerializeField] private Transform StartingPoint;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Transform[] hoverPoints;

    private currentWaypoint;


    

    private void Update()
    {
        
        MoveTowards(wayPoints[0]);
        if(Vector3.Distance(transform.position, wayPoints[0].pos) <= 1)
        {
            currentWaypoint = 1;
            HoverAround()
        }
        if (currentWaypoint == 1 && tilePlaced >= 5)
        {
            MoveTowards(wayPoints[1]);
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
