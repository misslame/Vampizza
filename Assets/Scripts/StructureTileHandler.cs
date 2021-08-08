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

    private static Dictionary<Vector3Int, StructureData> StructureData;
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
        if (!TileDictionary.ContainsKey(structure.GetType().ToString())){
            Debug.LogError("Stucture type not found!");
            TilemapStructures.SetTile(coord, TileDictionary["Invalid"]);
            return;
        }
        PlayerInventory.structureQuantities[structure.GetType().ToString()]--;
        TilemapStructures.SetTile(coord, TileDictionary[structure.GetType().ToString()]);
    }

    public static bool PutAwayStructure(Vector3Int coord){
        return false;
    }
}
