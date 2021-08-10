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

    public string mode = "Select";
    private Tilemap TilemapStructures;
    Vector3Int currentCell;
    Vector3Int lastCell;

    // Start is called before the first frame update
    void Start()
    {
        modes["Delete"] = DeleteModeToggle.GetComponent<Toggle>();
        TilemapStructures = GameGrid.transform.Find("TilemapStructures").GetComponent<Tilemap>();

        // eventually do this iteratively with each mode
        modes["Delete"].onValueChanged.AddListener(delegate(bool isOn){
            if (isOn){
                mode = "Delete";
            } else {
                mode = "Select";
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        bool click0 = Input.GetMouseButtonDown(0);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 noZ = new Vector3(pos.x, pos.y);
        currentCell = TilemapStructures.WorldToCell(noZ);
        if (click0 && mode == "Delete"){
            StructureTileHandler.PutAwayStructure(currentCell);
        }

        // handle color highlighting
        if (!currentCell.Equals(lastCell)){
            // Debug.Log(TilemapStructures.GetTile(currentCell));
            switch(mode){
                case "Select":
                    TilemapStructures.SetTileFlags(currentCell, TileFlags.None);
                    TilemapStructures.SetColor(currentCell, new Color(0.8f, 0.8f, 0.8f, 1f));
                    TilemapStructures.SetColor(lastCell, new Color(1f, 1f, 1f, 1f));
                    break;
                case "Delete":
                    TilemapStructures.SetTileFlags(currentCell, TileFlags.None);
                    TilemapStructures.SetColor(currentCell, new Color(1f, 0.3f, 0.3f, 1f));
                    TilemapStructures.SetColor(lastCell, new Color(1f, 1f, 1f, 1f));
                    break;
                default:
                    break;
            }
            lastCell = currentCell;
        }
    }


}
