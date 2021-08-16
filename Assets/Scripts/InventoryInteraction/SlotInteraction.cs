using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class SlotInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] RectTransform InventoryPanelSlider;
    [SerializeField] Grid TileGrids;
    [SerializeField] Tilemap TilemapStructures;
    [SerializeField] Tilemap TilemapPreview;
    [SerializeField] Tile TownHomeTile;
    [SerializeField] Tile HomeTile;
    [SerializeField] Tile FarmPlotTile;
    [SerializeField] GameObject InventoryGameObj;
    public Item SlotContent;
    Vector3Int LastDraggedOverCell;
    Tile SelectedTile;
    bool shopMode = false;

    public void Start(){ }

    // Event listener for mouse click down
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log(string.Format("You pressed the pointer down on <color=green>{0}</color>!\n" + 
        "This is a <color=purple>{1}</color>", SlotContent, SlotContent.GetType().BaseType));
        shopMode = InventoryPanelSlider.GetComponent<InventoryAndShopController>().ShopAndInventoryToggle.isOn;
        Debug.Log(string.Format("Shop mode <color={0}>{1}</color>", shopMode?"green":"red", shopMode.ToString()));

        if (shopMode && SlotContent.GetType().BaseType.ToString() == "Structure"){
            double newCurrency = Player.Instance.CurrentCurrency - ((Structure)SlotContent).GetPrice();
            if (newCurrency < 0){
                Debug.Log("<color=red>Not enough currency to buy that!</color>");
            } else {
                Player.Instance.SubtractCurrency(((Structure)SlotContent).GetPrice());
                Player.Instance.structureQuantities[SlotContent.GetType().ToString()]++;
                InventoryAndShopController.Instance.PopulateStructuresTab();
                Debug.Log(Player.Instance.CurrentCurrency);
                Player.Instance.currencyDisplay.SetCurrencyText(Player.Instance.CurrentCurrency);
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
            double maxWidth = Camera.main.pixelRect.width;
            double maxHeight = Camera.main.pixelRect.height;
            double cursorX = eventData.position.x;
            double cursorY = eventData.position.y;
            if (cursorX > maxWidth * 0.9){
                cursorX = maxWidth * 0.9;
            }
            if (cursorX < maxWidth - maxWidth * 0.95){
                cursorX = maxWidth - maxWidth * 0.95;
            }
            if (cursorY > maxHeight * 0.9){
                cursorY = maxHeight * 0.85;
            }
            if (cursorY < maxHeight - maxHeight * 0.95){
                cursorY = maxHeight - maxHeight * 0.95;
            }
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3((float)cursorX, (float)cursorY, Camera.main.nearClipPlane));
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
            // Using lastest cell coordinate that was dragged over in the preview grid, delete the
            // preview tile and update the structure tilegrid.
            TilemapPreview.SetTile(LastDraggedOverCell, null);

            // Use the StructureTileHandler to create a new structure
            StructureTileManager.Instance.BuildStructure(LastDraggedOverCell, (Structure)SlotContent);            
            
            InventoryAndShopController.Instance.PopulateStructuresTab();
        }
        Debug.Log("You have stopped dragging the pointer!");
    }
}