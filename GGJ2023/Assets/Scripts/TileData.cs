using UnityEngine;

// [System.Serializable]
// public class ConnectableEdge
// {
//     public bool isConnectable;
// }

[CreateAssetMenu(fileName = "TileData", menuName = "Tile Data")]
public class TileData : ScriptableObject
{
    public Sprite sprite;
    public bool[] edges = new bool[4];
}