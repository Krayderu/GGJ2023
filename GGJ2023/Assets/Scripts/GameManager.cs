using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private TileData[] tileTypes;
    [SerializeField] private RootsTilemap rootsTilemap;
    public CameraController cameraController;

    public static GameManager Instance { get; private set; }

    public int water = 100;
    [SerializeField] private float rootCost = 10f;
    public TileData currentTileType;
    public TileData nextTileType;

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

        currentTileType = tileTypes[0];
        nextTileType = tileTypes[0];
    }

    IEnumerator everySecond() {
        while (true) {
            DoResources();
            yield return new WaitForSeconds(1);
        }
    }

    public TileData GetNextTile(){
        currentTileType = nextTileType;

        int randomIndex = Random.Range(0, tileTypes.Length);
        TileData newTileType = tileTypes[randomIndex];

        nextTileType = newTileType;

        return newTileType;
    }

    private void DoResources(){
        int wateredRoots = rootsTilemap.GetNumberOfWateredRoots();

        water += wateredRoots;
    }

    public bool CanPay(){
        if (water - GetPrice(GetNumberOfRoots()) < 0) return false;
        return true;
    }

    public void Pay(){
        if(!CanPay()) {
            Debug.Log("Can't pay but pays anyway !!! Someone took advantage of a loophole ! BE THEY DAMNED !");
            return;
        }
        Debug.Log(GetPrice(GetNumberOfRoots()));
        water = water - GetPrice(GetNumberOfRoots());
    }

    public int GetNumberOfRoots(){
        return rootsTilemap.GetNumberOfRoots();
    }

    public int GetPrice(int n){
        return (int)Mathf.Floor(rootCost/2 * Mathf.Pow(n, 1.05f));
    }
}
