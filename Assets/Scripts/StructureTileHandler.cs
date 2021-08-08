using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureTileHandler : MonoBehaviour
{
    Tilemap TilemapStructures;
    [SerializeField] Tile TownHomeTile;
    [SerializeField] Tile HomeTile;
    [SerializeField] Tile FarmPlotTile;

    public Dictionary<Vector3Int, StructureData> Data;

    void Start(){
        TilemapStructures = GameObject.FindWithTag("Structures").GetComponent<Tilemap>();
    }

    static void CreateStructure(Vector3Int coord, Structure structure){

    }

    static bool PutAwayStructure(Vector3Int coord){
        return false;
    }
}
