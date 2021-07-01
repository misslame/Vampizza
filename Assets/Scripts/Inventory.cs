using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    // Unity Objects
    public GridLayoutGroup slotHolder;
    public GameObject firstSlot;

    // Inventory
    Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    // Start is called before the first frame update
    void Start() {

        /* DEBUG: TESTING USE ONLY REMOVE AFTER IMPLEMENTATION OF GATHERING RESOURCES */
        AddItemToInventory(new Wheat(3), "res");
        AddItemToInventory(new Tomato(2), "res");
        AddItemToInventory(new Blood(69), "res");
        AddItemToInventory(new Home(4.20), "stc");
        AddItemToInventory(new TownHome(69.00), "stc");
        AddItemToInventory(new FarmPlot(123.123), "stc");

        PopulateResourcesTab();

    }

    // Update is called once per frame
    void Update() { }

    void AddItemToInventory(Item i, string keyPrefix ) {
        
        inventory.Add((keyPrefix + inventory.Count.ToString()), i);
    }


    public void PopulateResourcesTab() {
        Debug.Log("populate Resources");
        
        Sprite newSprite;
        GameObject newSlot = new GameObject();

        EmptyInventoryPanel();

        foreach (KeyValuePair<string, Item> entry in inventory) {

            if (entry.Key.Contains("res")) {
                newSlot = Instantiate(firstSlot, transform);
                newSlot.transform.parent = slotHolder.transform;

                newSprite = Resources.Load<Sprite>(entry.Value.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;

                Resource cast = (Resource)entry.Value;
                newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "x" + cast.GetQuantity();
            }
        }

        Destroy(firstSlot);
        firstSlot = newSlot;
        
    }

    public void PopulateStructuresTab() {
        Debug.Log("populate Structures");
        
        Sprite newSprite;
        GameObject newSlot = new GameObject();

        EmptyInventoryPanel();

        
        foreach (KeyValuePair<string, Item> entry in inventory) {

            if (entry.Key.Contains("stc")) {
                newSlot = Instantiate(firstSlot, transform);
                newSlot.transform.parent = slotHolder.transform;

                newSprite = Resources.Load<Sprite>(entry.Value.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;

                newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = " ";
            }
        }

        Destroy(firstSlot);
        firstSlot = newSlot;
        
        
    }

    public void PopulateDecorTab() {

        /* DEBUG: temporary action. Will eventually implement official "empty list" */

        EmptyInventoryPanel();

        GameObject newSlot = Instantiate(firstSlot, transform);
        Destroy(firstSlot);

        firstSlot = newSlot;

    }

    void EmptyInventoryPanel() {
       
        int numberOfChildren = slotHolder.transform.childCount;
        Debug.Log(numberOfChildren);
        if (numberOfChildren > 1) {
            for (int i = 1; i < numberOfChildren; i++) {
                Destroy(slotHolder.transform.GetChild(i).gameObject);
            }

            firstSlot = slotHolder.transform.GetChild(0).gameObject;
        }
    }

}
