using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GameManager : MonoBehaviour {

    [SerializeField] private TileData[] tileTypes;
    [SerializeField] private RootsTilemap rootsTilemap;

    public static GameManager Instance { get; private set; }

    public int water = 100;
    [SerializeField] private int rootCost = 10;

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
    }

    private void Start()
    {
        StartCoroutine(everySecond());
    }

    IEnumerator everySecond() {
        while (true) {
            DoResources();
            yield return new WaitForSeconds(1);
        }
    }

    public TileData GetNextTile(){
        int randomIndex = Random.Range(0, tileTypes.Length);
        return tileTypes[randomIndex];
    }

    private void DoResources(){
        int wateredRoots = rootsTilemap.GetNumberOfWateredRoots();

        water += wateredRoots;
    }

    public bool CanPay(){
        if (water - rootCost < 0) return false;
        return true;
    }

    public void Pay(){
        if(!CanPay()) {
            Debug.Log("Can't pay but pays anyway !!! Someone took advantage of a loophole ! BE THEY DAMNED !");
            return;
        }
        water = water - rootCost;
    }
}
