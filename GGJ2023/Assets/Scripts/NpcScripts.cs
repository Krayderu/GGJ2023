using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State {GOING, HOVER, BACK, WITHSTUFF};

public class NpcScripts : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Transform[] hoverPoints;
    [SerializeField] private int rootsThreshold = 10;
    [SerializeField] private int minimumRoots = 0;

    private int currentWaypoint = 0;
    private int currentHoverPoint = 0;
    private State currentState = State.GOING;
    public int rootsPlaced = 1;

    

    private void Update()
    {
        if(rootsPlaced < minimumRoots) return;

        if (currentState == State.GOING){
            MoveTowards(wayPoints[currentWaypoint+1]);
            if (Vector3.Distance(transform.position, wayPoints[currentWaypoint+1].position) <= 1){
                currentWaypoint++;
                if (currentWaypoint == wayPoints.Length - 1){
                    currentState = State.HOVER;
                }
            }
        }
        else if (currentState == State.HOVER){
            int idx = (currentHoverPoint+1) % hoverPoints.Length;
            MoveTowards(hoverPoints[idx]);
            if (Vector3.Distance(transform.position, hoverPoints[idx].position) <= 1){
                currentHoverPoint = idx;
                
                // Wait (polish)
                // Flip sprite
                
            if (rootsPlaced >= rootsThreshold && currentHoverPoint == 0){
                currentState = State.BACK;
                // TODO Flip sprite horizontally
            }
            }
        }
        else if (currentState == State.BACK){
            MoveTowards(wayPoints[currentWaypoint-1]);
            if (Vector3.Distance(transform.position, wayPoints[currentWaypoint-1].position) <= 1){
                currentWaypoint--;
                if (currentWaypoint == 0){
                    currentState = State.WITHSTUFF;
                    // Wait (polish)
                    // Flip sprite
                }
            }

        }
        else if (currentState == State.WITHSTUFF){
             MoveTowards(wayPoints[currentWaypoint+1]);
            if (Vector3.Distance(transform.position, wayPoints[currentWaypoint+1].position) <= 1){
                currentWaypoint++;
                if (currentWaypoint == wayPoints.Length - 1){
                    //Enable Sprites in the room
                    //Change Materials for the room
                }
            }
        }
    }

        
        //MoveTowards(wayPoints[1]);
        // if(Vector3.Distance(transform.position, wayPoints[1].position) <= 1 %% )
        // {
        //     currentWaypoint = 1;
        //     HoverAround();
        // }
        // if (currentWaypoint == 1 && tilePlaced >= 5)
        // {
        //     MoveTowards(wayPoints[0]);
        // }
    

    private void MoveTowards(Transform pos)
    {
        Vector3 destination = pos.position;
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
    }

    private void HoverAround()
    {
        //move in between a set number of points
    }
}
