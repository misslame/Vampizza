using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class StructureModeHandler : MonoBehaviour
{
    [SerializeField] GameObject DeleteModeToggle;
    [SerializeField] Grid GameGrid;

    public Dictionary<string, Toggle> modes = new Dictionary<string, Toggle>()
    {
        {"Delete", null}
    };

    public string mode = "";
    private Tilemap TilemapStructures;

    void UpdateMode(string mode){
        this.mode = mode; 
    }

    // Start is called before the first frame update
    void Start()
    {
        modes["Delete"] = DeleteModeToggle.GetComponent<Toggle>();
        TilemapStructures = GameGrid.transform.Find("TilemapStructures").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(mode){
            case "Delete":
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 noZ = new Vector3(pos.x, pos.y);
                Vector3Int currentCell = TilemapStructures.WorldToCell(noZ);
                TilemapStructures.GetTile(currentCell);
                break;
            default:
                break;
        }
    }
}
