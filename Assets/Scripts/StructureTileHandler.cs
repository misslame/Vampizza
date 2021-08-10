using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureTileHandler : MonoBehaviour
{
    static Tilemap TilemapStructures;
    [SerializeField] Tile TownHomeTile;
    [SerializeField] Tile HomeTile;
    [SerializeField] Tile FarmPlotTile;
    [SerializeField] Tile InvalidTile;

    private static Dictionary<Vector3Int, StructureData> StructureData = new Dictionary<Vector3Int,StructureData>();
    private static Dictionary<string, Tile> TileDictionary;
    void Start(){
        TilemapStructures = GameObject.FindWithTag("Structures").GetComponent<Tilemap>();
        TileDictionary = new Dictionary<string, Tile>()
        {
            {"Home", HomeTile},
            {"TownHome", HomeTile},
            {"FarmPlot", FarmPlotTile},
            {"Invalid", InvalidTile}
        };
    }

    public static void CreateStructure(Vector3Int coord, Structure structure){
        if (GetStructureData(coord) != null){
            PutAwayStructure(coord);
        }
        string structureString = structure.GetType().ToString();
        Debug.Log(structureString);
        if (!TileDictionary.ContainsKey(structure.GetType().ToString())){
            Debug.LogError("Stucture type not found!");
            TilemapStructures.SetTile(coord, TileDictionary["Invalid"]);
            return;
        }
        Inventory.structureQuantities[structure.GetType().ToString()]--;
        TilemapStructures.SetTile(coord, TileDictionary[structureString]);
        StructureData.Add(coord, new StructureData(structure, TileDictionary[structureString]));
        
        // debug only xD hahaha lololol!11!!!!!!!!!1111 :3 💯💯💯💯
        foreach (KeyValuePair<Vector3Int,StructureData> item in StructureData){
            Debug.Log(GetStructureData(new Vector3Int(-500, 0, 0)));
        }
    }

    public delegate void PutAwayStructureDelegate();
    public static bool PutAwayStructure(Vector3Int coord, PutAwayStructureDelegate d){
        Debug.Log(coord);
        StructureData data = GetStructureData(coord);
        if (data == null){
            return false;
        }
        Inventory.structureQuantities[data.structure.GetType().ToString()]++;
        TilemapStructures.SetTile(coord, null);
        StructureData.Remove(coord);
        d();
        return true;
    }

    public static bool PutAwayStructure(Vector3Int coord){
        Debug.Log(coord);
        StructureData data = GetStructureData(coord);
        if (data == null){
            return false;
        }
        Inventory.structureQuantities[data.structure.GetType().ToString()]++;
        TilemapStructures.SetTile(coord, null);
        StructureData.Remove(coord);
        return true;
    }

    public static StructureData GetStructureData(Vector3Int coord){
        try {
            return StructureData[coord];
        }
        catch(System.Exception e){
            return null;
        }
    }
}
