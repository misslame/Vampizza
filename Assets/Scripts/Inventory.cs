using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    // Singleton stuff
    private static Inventory instance = null;
    public static Inventory Instance { // MUST BE READ-ONLY DO NOT ADD A SET PROPERTY. 
        get { return instance; } 
    }

    // Unity Objects
    public GridLayoutGroup slotHolder;
    public GameObject slotInventoryCopy;
    public GameObject slotShopCopy;

    // Inventory
    Dictionary<string, Item> inventory = new Dictionary<string, Item>();
    public string currentTab;

    
    public Dictionary<string, int> structureQuantities = new Dictionary<string, int>
    {
        {"TownHome", 2},
        {"FarmPlot", 3}
    };

    void Awake(){
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        hideSlot(slotInventoryCopy);
        hideSlot(slotShopCopy);
        /* DEBUG: TESTING USE ONLY REMOVE AFTER IMPLEMENTATION OF GATHERING RESOURCES */
        AddItemToInventory(new Wheat(3), "res");
        AddItemToInventory(new Tomato(2), "res");
        AddItemToInventory(new Blood(69), "res");
        AddItemToInventory(new TownHome(200), "stc");
        AddItemToInventory(new FarmPlot(100.00), "stc");

        PopulateResourcesTab();

    }

    // Use to hide slotCopy, which will persist
    void hideSlot(GameObject slot){
        // Hide component via set alpha to 0 and prevent receiving of input events
        slot.GetComponent<CanvasGroup>().alpha = 0f;
        slot.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Use to show newly cloned slots derived from slotCopy, which will eventually be deleted
    void showSlot(GameObject slot){
        // Show component via set alpha to 1 and allow receiving of input events
        slot.GetComponent<CanvasGroup>().alpha = 1f;
        slot.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void AddItemToInventory(Item i, string keyPrefix ) {
        inventory.Add((keyPrefix + inventory.Count.ToString()), i);
    }


    public void PopulateResourcesTab() {
        currentTab = "Resources";
        Debug.Log("populate Resources");
        
        Sprite newSprite;
        GameObject newSlot;

        EmptyInventoryPanel();

        foreach (KeyValuePair<string, Item> entry in inventory) {

            if (entry.Key.Contains("res")) {
                newSlot = Instantiate(slotInventoryCopy, transform);
                Debug.Log(entry.Value);
                newSlot.GetComponent<SlotInteraction>().SlotContent = entry.Value;
                showSlot(newSlot);
                newSlot.transform.parent = slotHolder.transform;

                newSprite = Resources.Load<Sprite>(entry.Value.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().preserveAspect = true;

                Resource cast = (Resource)entry.Value;
                newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "x" + cast.GetQuantity();
            }
        }

        
    }

    public void PopulateStructuresTab() {
        currentTab = "Structures";
        EmptyInventoryPanel();

        bool shopMode = this.GetComponentInParent<InventoryAndShopController>().toggleShopOrInventory.isOn;

        if (shopMode) {
            PopulateStructuresTabToShop(slotShopCopy);
        } else {
            PopulateStructuresTabToInventory(slotInventoryCopy);
        }
        
    }

    private void PopulateStructuresTabToShop(GameObject slot) {
        Debug.Log("populate Structures: Shop");

        Sprite newSprite;
        GameObject newSlot;

        foreach (KeyValuePair<string, Item> entry in inventory) {
            if(entry.Key.Contains("stc")) {
                //Create a new slot gameobj from slotCopy, then put the corresponding item object into DragDrop.SlotContent
                newSlot = Instantiate(slot, transform);
                newSlot.GetComponent<SlotInteraction>().SlotContent = entry.Value;

                // Show slot, then update transform
                showSlot(newSlot);
                newSlot.transform.parent = slotHolder.transform;

                // Do sprite stuff
                newSprite = Resources.Load<Sprite>(entry.Value.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;

                slotHolder.cellSize = new Vector2(250f, 80f);

                newSlot.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = ((Structure)entry.Value).GetPrice().ToString();
                newSlot.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().text = structureQuantities[entry.Value.GetType().ToString()].ToString();

                // Fancy Color changing for the text for feedback. 
                if(Player.Instance.CurrentCurrency >= ((Structure)entry.Value).GetPrice()) {
                    newSlot.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.green; // If player can afford, make text color green. 
                } else {
                    newSlot.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red; // If player cannot afford, make text color red. 
                }

                if(structureQuantities[entry.Value.GetType().ToString()] <= 0) {
                    newSlot.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red; // If they have 0 of this item in their inventory. 
                }
            }
        }

    }

    private void PopulateStructuresTabToInventory(GameObject slot) {
        Debug.Log("populate Structures: Inventory");

        Sprite newSprite;
        GameObject newSlot;

        foreach (KeyValuePair<string, Item> entry in inventory) {

            if (entry.Key.Contains("stc")) {
                if (structureQuantities[entry.Value.GetType().ToString()] == 0) {
                    continue;
                }

                //Create a new slot gameobj from slotCopy, then put the corresponding item object into DragDrop.SlotContent
                newSlot = Instantiate(slot, transform);
                newSlot.GetComponent<SlotInteraction>().SlotContent = entry.Value;

                // Show slot, then update transform
                showSlot(newSlot);
                newSlot.transform.parent = slotHolder.transform;

                slotHolder.cellSize = new Vector2(80f, 80f);

                // Do sprite stuff
                newSprite = Resources.Load<Sprite>(entry.Value.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;
                newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "x" + structureQuantities[entry.Value.GetType().ToString()];
            }
        }
    }

    public void PopulateDecorTab() {

        /* DEBUG: temporary action. Will eventually implement official "empty list" */

        EmptyInventoryPanel();

        // GameObject newSlot = Instantiate(slotCopy, transform);
        // newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = " ";
    }

    void EmptyInventoryPanel() {
       
        int numberOfChildren = slotHolder.transform.childCount;
        // Debug.Log(numberOfChildren);
        if (numberOfChildren >= 1) {
            // Delete all children of SlotHolder
            for (int i = 0; i < numberOfChildren; i++) {
                Destroy(slotHolder.transform.GetChild(i).gameObject);
            }
        }
    }

}
