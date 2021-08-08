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
    private static Inventory PlayerInventory;

    void Start(){
        TilemapStructures = GameObject.FindWithTag("Structures").GetComponent<Tilemap>();
        TileDictionary = new Dictionary<string, Tile>()
        {
            {"Home", HomeTile},
            {"TownHome", HomeTile},
            {"FarmPlot", FarmPlotTile},
            {"Invalid", InvalidTile}
        };
        PlayerInventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    public static void CreateStructure(Vector3Int coord, Structure structure){
        string structureString = structure.GetType().ToString();
        Debug.Log(structureString);
        if (!TileDictionary.ContainsKey(structure.GetType().ToString())){
            Debug.LogError("Stucture type not found!");
            TilemapStructures.SetTile(coord, TileDictionary["Invalid"]);
            return;
        }
        PlayerInventory.structureQuantities[structure.GetType().ToString()]--;
        TilemapStructures.SetTile(coord, TileDictionary[structureString]);
        StructureData.Add(coord, new StructureData(structure, TileDictionary[structureString]));
        
        // debug only xD hahaha lololol!11!!!!!!!!!1111 :3 💯💯💯💯
        foreach (KeyValuePair<Vector3Int,StructureData> item in StructureData){
            Debug.Log(item);
        }
    }

    public static bool PutAwayStructure(Vector3Int coord){
        return false;
    }
}
