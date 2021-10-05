using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Animations;

public class StructureTileManager : MonoBehaviour
{
    private Tilemap TilemapStructures;
    [SerializeField] private Tile InvalidTile;

    private static StructureTileManager instance = null;
    public static StructureTileManager Instance {
        get {return instance;}
    }

    private Dictionary<Vector3Int, Structure> StructureData = new Dictionary<Vector3Int,Structure>();
    private List<string> TileList;

    private void Awake(){

        // singleton stuff 
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start(){
        // Keeps track of structure objects within the grid
        TilemapStructures = GameObject.FindWithTag("Structures").GetComponent<Tilemap>();

        // For matching corresponding structure object with tile
        // @TODO: maybe store references to tiles within structure class definitions
        TileList = new List<string>()
        {
            "Home", 
            "TownHome",
            "FarmPlot", 
        };
    }

    /// <summary>
    /// Build structure from inventory. 
    /// </summary>
    public void BuildStructure(Vector3Int coord, Structure structure){
        string structureString = structure.GetType().ToString();
        // Quantity check
        if (Player.Instance.structureQuantities[structureString] <= 0){
            Debug.Log("You don't have enough of this structure");
            return;
        }

        // If there's a structure already in the spot, put it away.
        if (GetStructureData(coord) != null){
            PutAwayStructure(coord);
        }

        // Handle Structure not having an entry in the TileDictionary
        if (!TileList.Contains(structureString)){
            Debug.LogError("Stucture type not found!");
            TilemapStructures.SetTile(coord, InvalidTile);
            return;
        }

        if (!structure.GetGameObjectURL().Equals("")){
            structure.LoadGameObject();
            structure.GetTile().gameObject.transform.position = TilemapStructures.layoutGrid.CellToWorld(coord);
        } 
        
        // Place Tile, add it to StructureData dictionary, decrement inventory quantity
        TilemapStructures.SetTile(coord, structure.GetTile());
        StructureData.Add(coord, structure);
        Player.Instance.structureQuantities[structureString]--;
    }

    /// <summary>
    /// Puts away a structure into inventory for a given coord. Returns false if no structure is found.
    /// </summary>
    public bool PutAwayStructure(Vector3Int coord){
        Structure data = GetStructureData(coord);
        if (data == null){
            return false;
        }
        Player.Instance.structureQuantities[data.GetType().ToString()]++;
        TilemapStructures.SetTile(coord, null);
        StructureData.Remove(coord);
        return true;
    }

    /// <summary>
    /// Returns StructureData object on given cell coordinate. Returns null if no structure is found.
    /// </summary>
    public Structure GetStructureData(Vector3Int coord){
        try {
            return StructureData[coord];
        }
        catch(System.Exception e){
            return null;
        }
    }

    public void TryHarvest(Vector3Int coord){
        Structure structure = GetStructureData(coord);
        if (structure == null){
            Debug.Log("harvested structure came up as null");
            return;
        }
        if (structure.GetType() != typeof(FarmPlot)){
            Debug.Log("This isn't a crop");
            return;
        }
        CropController controller = TilemapStructures.GetInstantiatedObject(coord).GetComponent<CropController>();
        if (!controller.isDoneGrowing()){
            Debug.Log("not done growing");
            return;
        }
        controller.harvest();
    }
}
