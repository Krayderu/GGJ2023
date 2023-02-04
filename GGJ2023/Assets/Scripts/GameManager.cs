using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GameManager : MonoBehaviour {

    [SerializeField] private TileData[] tileTypes;

    public static GameManager Instance { get; private set; }

    private void Awake()
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }

        //Debug.Log(tileTypes);
    }

    public TileData GetNextTile(){
        int randomIndex = Random.Range(0, tileTypes.Length);
        return tileTypes[randomIndex];
    }
}
