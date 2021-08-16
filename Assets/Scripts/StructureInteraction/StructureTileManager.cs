using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureTileManager : MonoBehaviour
{
    static Tilemap TilemapStructures;
    [SerializeField] Tile TownHomeTile;
    [SerializeField] Tile HomeTile;
    [SerializeField] Tile FarmPlotTile;
    [SerializeField] Tile InvalidTile;

    private static StructureTileManager instance = null;
    public static StructureTileManager Instance {
        get {return instance;}
    }

    private Dictionary<Vector3Int, StructureData> StructureData = new Dictionary<Vector3Int,StructureData>();
    private Dictionary<string, Tile> TileDictionary;

    private void Awake(){
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
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

    public void BuildStructure(Vector3Int coord, Structure structure){
        if (Player.Instance.structureQuantities[structure.GetType().ToString()] <= 0){
            Debug.Log("You don't have enough of this structure");
            return;
        }
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
        Player.Instance.structureQuantities[structure.GetType().ToString()]--;
        TilemapStructures.SetTile(coord, TileDictionary[structureString]);
        StructureData.Add(coord, new StructureData(structure, TileDictionary[structureString]));
    }

    public delegate void PutAwayStructureDelegate();
    public bool PutAwayStructure(Vector3Int coord, PutAwayStructureDelegate d){
        Debug.Log(coord);
        StructureData data = GetStructureData(coord);
        if (data == null){
            return false;
        }
        Player.Instance.structureQuantities[data.structure.GetType().ToString()]++;
        TilemapStructures.SetTile(coord, null);
        StructureData.Remove(coord);
        d();
        return true;
    }

    public bool PutAwayStructure(Vector3Int coord){
        Debug.Log(coord);
        StructureData data = GetStructureData(coord);
        if (data == null){
            return false;
        }
        Player.Instance.structureQuantities[data.structure.GetType().ToString()]++;
        TilemapStructures.SetTile(coord, null);
        StructureData.Remove(coord);
        return true;
    }

    public StructureData GetStructureData(Vector3Int coord){
        try {
            return StructureData[coord];
        }
        catch(System.Exception e){
            return null;
        }
    }
}
