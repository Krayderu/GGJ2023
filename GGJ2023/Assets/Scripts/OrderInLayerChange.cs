using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayerChange : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(npc.transform.position.z >= transform.position.z)
        {
            spriteRenderer.sortingOrder = npc.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else if (npc.transform.position.z <= transform.position.z)
        {
            spriteRenderer.sortingOrder = npc.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
    }
}
