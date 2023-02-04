using UnityEngine;


public class RootTile : MonoBehaviour
{
    public TileData tileData;
    public ConnectableEdge[] edges = new ConnectableEdge[4];

    void Start()
    {
        edges = tileData.edges;
    }
}
