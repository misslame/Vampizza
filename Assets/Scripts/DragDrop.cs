using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.Animations;
using UnityEngine.Tilemaps;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] RectTransform InventoryPanelSlider;
    [SerializeField] Grid TileGrids;
    [SerializeField] Tilemap TilemapStructures;
    [SerializeField] Tilemap TilemapPreview;
    [SerializeField] Tile TownHomeTile;
    [SerializeField] Tile HomeTile;
    [SerializeField] Tile FarmPlotTile;
    public Item SlotContent;
    Vector3Int LastDraggedOverCell;
    Tile SelectedTile;


    // Start is called before the first frame update
    void Start(){
        // for (int i = 0; i < TileGrids.transform.childCount; i++){
        //     if (TileGrids.transform.GetChild(i).name.Equals("TilemapStructures"))
        //         Debug.Log("groovy :)");
        // }
    }

    // Event listener for mouse click down
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log(string.Format("You pressed the pointer down on <color=green>{0}</color>!\n" + 
        "This is a <color=purple>{1}</color>", SlotContent, SlotContent.GetType().BaseType));
    }

    // Event listener for beginning of drag
    public void OnBeginDrag(PointerEventData eventData) {
        Animator anim = InventoryPanelSlider.GetComponent<Animator>();
        anim.SetBool("ShowInventory", false);
        Debug.Log("You are now dragging the pointer!");
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        Vector3 worldPointInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(worldPoint);
        Vector3 noZ = new Vector3(worldPoint.x, worldPoint.y);
        LastDraggedOverCell = TileGrids.WorldToCell(noZ);
    }

    // Event listener for drag, triggers for every frame that drag occurs
    public void OnDrag(PointerEventData eventData) {
        // Debug.Log("You dragged for a frame!");
        
        // Converts cursor drag position to point in world. 
        // It would be nice if there was a more efficient or elegant way to do this
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        Vector3 worldPointInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(worldPoint);
        Vector3 noZ = new Vector3(worldPoint.x, worldPoint.y);
        Vector3Int currentCell = TileGrids.WorldToCell(noZ);
        if (!currentCell.Equals(LastDraggedOverCell)){
            TilemapPreview.SetTile(LastDraggedOverCell, null);
            switch(SlotContent.GetType().ToString()){
                case "FarmPlot":
                    TilemapPreview.SetTile(currentCell, FarmPlotTile);
                    SelectedTile = FarmPlotTile;
                    break;
                case "Home":
                    TilemapPreview.SetTile(currentCell, HomeTile);
                    SelectedTile = HomeTile;
                    break;
                case "TownHome":
                    TilemapPreview.SetTile(currentCell, TownHomeTile);
                    SelectedTile = TownHomeTile;
                    break;
                default:
                    break;
            }
        }
        
        LastDraggedOverCell = currentCell;
    }

    // Event listener for end of drag
    public void OnEndDrag(PointerEventData eventData) {
        TilemapPreview.SetTile(LastDraggedOverCell, null);
        TilemapStructures.SetTile(LastDraggedOverCell, SelectedTile);
        Debug.Log("You have stopped dragging the pointer!");
    }
}
