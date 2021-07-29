using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SlotInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] RectTransform InventoryPanelSlider;
    [SerializeField] Grid TileGrids;
    [SerializeField] Tilemap TilemapStructures;
    [SerializeField] Tilemap TilemapPreview;
    [SerializeField] Tile TownHomeTile;
    [SerializeField] Tile HomeTile;
    [SerializeField] Tile FarmPlotTile;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject InventoryGameObj;
    [SerializeField] GameObject TextGameObj;
    public Item SlotContent;
    Vector3Int LastDraggedOverCell;
    Tile SelectedTile;
    bool shopMode = false;

    public void Start(){ }

    // Event listener for mouse click down
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log(string.Format("You pressed the pointer down on <color=green>{0}</color>!\n" + 
        "This is a <color=purple>{1}</color>", SlotContent, SlotContent.GetType().BaseType));
        shopMode = InventoryPanelSlider.GetComponent<InventoryAndShopController>().toggleShopOrInventory.isOn;
        Debug.Log(string.Format("Shop mode <color={0}>{1}</color>", shopMode?"green":"red", shopMode.ToString()));
        Player player = Player.GetComponent<Player>();

        if (shopMode && SlotContent.GetType().BaseType.ToString() == "Structure"){
            long newCurrency = player.currency - ((Structure)SlotContent).cost;
            if (newCurrency < 0){
                Debug.Log("<color=red>Not enough currency to buy that!</color>");
            } else {
                player.currency -= ((Structure)SlotContent).cost;
                InventoryGameObj.GetComponent<Inventory>().structureQuantities[SlotContent.GetType().ToString()]++;
                InventoryGameObj.GetComponent<Inventory>().PopulateStructuresTab();
                Debug.Log(player.currency);
                player.currencyDisplay.SetCurrencyText(player.currency);
            }
        }
    }

    // Event listener for beginning of drag
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("You are now dragging the pointer!");


        if (!shopMode && SlotContent.GetType().BaseType.ToString() == "Structure"){
            // Get animator component, then hide slider
            Animator anim = InventoryPanelSlider.GetComponent<Animator>();
            anim.SetBool("ShowInventory", false);

            // Convert cursor screen pos. to world pos., then convert + save world pos. into grid cell coord
            // It would be nice if there was a more efficient or elegant way to do this
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
            Vector3 noZ = new Vector3(worldPoint.x, worldPoint.y);
            LastDraggedOverCell = TileGrids.WorldToCell(noZ);
        }
    }

    // Event listener for drag, triggers for every frame that drag occurs
    public void OnDrag(PointerEventData eventData) {
        // Debug.Log("You dragged for a frame!");
        
        if (!shopMode){
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
    }

    // Event listener for end of drag
    public void OnEndDrag(PointerEventData eventData) {
        
        if (!shopMode && SlotContent.GetType().BaseType.ToString() == "Structure"){
            Inventory inventory = InventoryGameObj.GetComponent<Inventory>();
            // Using lastest cell coordinate that was dragged over in the preview grid, delete the
            // preview tile and update the structure tilegrid.
            TilemapPreview.SetTile(LastDraggedOverCell, null);
            TilemapStructures.SetTile(LastDraggedOverCell, SelectedTile);
            inventory.structureQuantities[SlotContent.GetType().ToString()]--;
            // this.transform.GetChild(1).gameObject.GetComponent<Text>().text = "x" + inventory.structureQuantities[SlotContent.GetType().ToString()];
            inventory.PopulateStructuresTab();
        }
        Debug.Log("You have stopped dragging the pointer!");
    }
}
