using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Tile Data")]
public class TileData : ScriptableObject
{
    public Sprite sprite;
    public bool[] edges = new bool[4];
}