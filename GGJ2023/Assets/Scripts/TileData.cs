using UnityEngine;

[System.Serializable]
public class ConnectableEdge
{
    public bool isConnectable;
}

[CreateAssetMenu(fileName = "TileData", menuName = "Tile Data")]
public class TileData : ScriptableObject
{
    public Sprite sprite;
    public ConnectableEdge[] edges = new ConnectableEdge[4];
}