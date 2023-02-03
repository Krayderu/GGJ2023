using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScripts : MonoBehaviour
{
    [SerializeField] private Transform StartingPoint;
    [SerializeField] private Transform Step1pos;
    [SerializeField] private Transform Step2pos;
    [SerializeField] private Transform Step3pos;

    [SerializeField] private bool step1 = false;
    [SerializeField] private float comfort = 0f;
    

    private void Update()
    {
        
        MoveTowards(Step1pos);
        if(Vector3.Distance(transform.position, Step1pos.position) <= 1)
        {
            step1 = true;
            HoverAround(Step1pos);
        }
        if (step1 = true && comfort >= 2)
        {
            MoveTowards(Step2pos);
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
