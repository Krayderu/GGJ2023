using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State {GOING, HOVER, BACK, WITHSTUFF, HOME};

public class NpcScripts : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Transform[] hoverPoints;
    [SerializeField] private int rootsThreshold = 10;
    [SerializeField] private int minimumRoots = 0;
    [SerializeField] private string roomTag;
    [SerializeField] private Transform roomHeight;

    private int currentWaypoint = 0;
    private int currentHoverPoint = 0;
    private State currentState = State.GOING;
    //public int rootsPlaced = 1;
    private SpriteRenderer sr;
    //private bool flipping = false;
    private int facingDirection = 1;
    //private Quaternion lastRotation;

    //private Dictionary<string, Material> materials

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        transform.position = wayPoints[0].position;
        //lastRotation = transform.rotation;
    }
    

    private void Update()
    {
        int rootsPlaced = GameManager.Instance.GetNumberOfRoots();
        
        if(rootsPlaced < minimumRoots) return;

        if (currentState == State.GOING){
            var nextWaypoint = wayPoints[currentWaypoint+1];
            MoveTowards(nextWaypoint);
            if (Vector3.Distance(transform.position, nextWaypoint.position) <= 1){
                currentWaypoint++;
                if (currentWaypoint == wayPoints.Length - 1){
                    currentState = State.HOVER;
                }
            }
            if (Mathf.Sign(facingDirection) != Mathf.Sign(nextWaypoint.position.x - transform.position.x)){
                facingDirection *= -1;
                sr.flipX = facingDirection > 0;
            }
        }
        else if (currentState == State.HOVER){
            int idx = (currentHoverPoint+1) % hoverPoints.Length;
            var nextWaypoint = hoverPoints[idx];
            MoveTowards(nextWaypoint);
            if (Vector3.Distance(transform.position, nextWaypoint.position) <= 1){
                currentHoverPoint = idx;
                
                // Wait (polish)

                if (rootsPlaced >= rootsThreshold && currentHoverPoint == 0){
                    currentState = State.BACK;
                }
            }
            if (Mathf.Sign(facingDirection) != Mathf.Sign(nextWaypoint.position.x - transform.position.x)){
                facingDirection *= -1;
                sr.flipX = facingDirection > 0;
            }
        }
        else if (currentState == State.BACK){
            var nextWaypoint = wayPoints[currentWaypoint-1];
            MoveTowards(nextWaypoint);
            if (Vector3.Distance(transform.position, nextWaypoint.position) <= 1){
                currentWaypoint--;
                if (currentWaypoint == 0){
                    currentState = State.WITHSTUFF;
                    // Wait (polish)
                    // Flip sprite
                    sr.flipX = true;
                    // show cart
                    if (transform.childCount > 0){
                        var child = transform.GetChild(0).gameObject;
                        child.SetActive(true);
                    }

                    // move camera over ground:
                    GameManager.Instance.cameraController.moveToY(roomHeight.position.y);
                }
            }
            if (Mathf.Sign(facingDirection) != Mathf.Sign(nextWaypoint.position.x - transform.position.x)){
                facingDirection *= -1;
                sr.flipX = facingDirection > 0;
            }
        }
        else if (currentState == State.WITHSTUFF){
            
            // do the same as GOING
            var nextWaypoint = wayPoints[currentWaypoint+1];
            MoveTowards(nextWaypoint);
            if (Vector3.Distance(transform.position, nextWaypoint.position) <= 1){
                currentWaypoint++;
                if (currentWaypoint == wayPoints.Length - 1){
                    currentState = State.HOME;
                    // fill room
                    fillRoom(roomTag);
                    // hide box
                    if (transform.childCount > 0){
                        var child = transform.GetChild(0).gameObject;
                        child.SetActive(false);
                    }
                    
                }
            }
            if (Mathf.Sign(facingDirection) != Mathf.Sign(nextWaypoint.position.x - transform.position.x)){
                facingDirection *= -1;
                sr.flipX = facingDirection > 0;
            }
        }
        else if (currentState == State.HOME){
            // Hover
            int idx = (currentHoverPoint+1) % hoverPoints.Length;
            var nextWaypoint = hoverPoints[idx];
            MoveTowards(nextWaypoint);
            if (Vector3.Distance(transform.position, nextWaypoint.position) <= 1){
                currentHoverPoint = idx;
                // FOREVAH
            }
            if (Mathf.Sign(facingDirection) != Mathf.Sign(nextWaypoint.position.x - transform.position.x)){
                facingDirection *= -1;
                sr.flipX = facingDirection > 0;
            }
        }
    }
    

    private void MoveTowards(Transform pos)
    {
        Vector3 destination = pos.position;
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
    }


    private void fillRoom(string tag){
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag)){
            Debug.Log(obj);
            var materialChanger = obj.GetComponent<MaterialToChangeTo>();
            if (materialChanger){
                Debug.Log("Changing to " + materialChanger.materialToChangeTo.name);
                // load material
                var material = Resources.Load<Material>(materialChanger.materialToChangeTo.name);
                Debug.Log(material);
                var meshRenderer = obj.GetComponent<MeshRenderer>();
                if (meshRenderer) meshRenderer.material = material;
            }

            //show sprite and if there is the child light
            var sr = obj.GetComponent<SpriteRenderer>();
            if (sr){
                sr.enabled = true;
                if (obj.transform.childCount > 0){
                    var light = obj.transform.GetChild(0).gameObject;
                    light.SetActive(true);
                }
            }
        }
    }

    // private void Flip(){
    //     transform.rotation = Quaternion.Lerp(transform.rotation, lastRotation.Euler(0, 180, 0), timeCount * speed);
    //     timeCount = timeCount + Time.deltaTime;
    // }
}
