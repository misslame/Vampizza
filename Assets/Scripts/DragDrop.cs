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

    // Event listener for mouse click down
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log(string.Format("You pressed the pointer down on <color=green>{0}</color>!\n" + 
        "This is a <color=purple>{1}</color>", SlotContent, SlotContent.GetType().BaseType));
    }

    // Event listener for beginning of drag
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("You are now dragging the pointer!");

        // Get animator component, then hide slider
        Animator anim = InventoryPanelSlider.GetComponent<Animator>();
        anim.SetBool("ShowInventory", false);

        // Convert cursor screen pos. to world pos., then convert + save world pos. into grid cell coord
        // It would be nice if there was a more efficient or elegant way to do this
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        Vector3 noZ = new Vector3(worldPoint.x, worldPoint.y);
        LastDraggedOverCell = TileGrids.WorldToCell(noZ);
    }

    // Event listener for drag, triggers for every frame that drag occurs
    public void OnDrag(PointerEventData eventData) {
        // Debug.Log("You dragged for a frame!");
        
        // Convert cursor screen pos. to world pos., then convert + save world pos. into grid cell coord
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        Vector3 noZ = new Vector3(worldPoint.x, worldPoint.y);
        Vector3Int currentCell = TileGrids.WorldToCell(noZ);

        // If mouse goes over a new cell, update the preview TileGrid with the correct tile
        if (!currentCell.Equals(LastDraggedOverCell)){
            TilemapPreview.SetTile(LastDraggedOverCell, null);

            // There's probably a better way to do this that isn't a switch statement
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
        
        // Update last cell that was dragged over
        LastDraggedOverCell = currentCell;
    }

    // Event listener for end of drag
    public void OnEndDrag(PointerEventData eventData) {

        // Using lastest cell coordinate that was dragged over in the preview grid, delete the
        // preview tile and update the structure tilegrid.
        TilemapPreview.SetTile(LastDraggedOverCell, null);
        TilemapStructures.SetTile(LastDraggedOverCell, SelectedTile);
        Debug.Log("You have stopped dragging the pointer!");
    }
}