using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    // Unity Objects
    public GridLayoutGroup slotHolder;
    public GameObject slotCopy;

    // Inventory
    Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    // Start is called before the first frame update
    void Start() {
        hideSlot(slotCopy);
        /* DEBUG: TESTING USE ONLY REMOVE AFTER IMPLEMENTATION OF GATHERING RESOURCES */
        AddItemToInventory(new Wheat(3), "res");
        AddItemToInventory(new Tomato(2), "res");
        AddItemToInventory(new Blood(69), "res");
        AddItemToInventory(new TownHome(69.00), "stc");
        AddItemToInventory(new FarmPlot(123.123), "stc");

        PopulateResourcesTab();

    }

    // Update is called once per frame
    void Update() { }

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
        Debug.Log("populate Resources");
        
        Sprite newSprite;
        GameObject newSlot;

        EmptyInventoryPanel();

        foreach (KeyValuePair<string, Item> entry in inventory) {

            if (entry.Key.Contains("res")) {
                newSlot = Instantiate(slotCopy, transform);
                Debug.Log(entry.Value);
                newSlot.GetComponent<SlotInteraction>().SlotContent = entry.Value;
                showSlot(newSlot);
                newSlot.transform.parent = slotHolder.transform;

                newSprite = Resources.Load<Sprite>(entry.Value.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;

                Resource cast = (Resource)entry.Value;
                newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "x" + cast.GetQuantity();
            }
        }

        
    }

    public void PopulateStructuresTab() {
        Debug.Log("populate Structures");
        
        Sprite newSprite;
        GameObject newSlot;

        EmptyInventoryPanel();

        
        foreach (KeyValuePair<string, Item> entry in inventory) {

            if (entry.Key.Contains("stc")) {
                //Create a new slot gameobj from slotCopy, then put the corresponding item object into DragDrop.SlotContent
                newSlot = Instantiate(slotCopy, transform);
                newSlot.GetComponent<SlotInteraction>().SlotContent = entry.Value;

                // Show slot, then update transform
                showSlot(newSlot);
                newSlot.transform.parent = slotHolder.transform;

                // Do sprite stuff
                newSprite = Resources.Load<Sprite>(entry.Value.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;
                newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = " ";
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
