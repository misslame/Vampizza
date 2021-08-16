using UnityEngine.Tilemaps;

public class StructureData{
    public Structure structure;
    public Tile tile;
    public StructureData(Structure s, Tile t){
        structure = s;
        tile = t;
    }
}