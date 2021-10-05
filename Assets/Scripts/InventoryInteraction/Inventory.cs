using System.Collections.Generic;
using UnityEngine;


public class ItemInventory : List<Item> {
    public void AddItem(Item item) {
        base.Add(item);
    }


    public void Init() {
        AddItem(new Wheat(3));
        AddItem(new Tomato(2));
        AddItem(new Blood(69));
        AddItem(new TownHome(200));
        AddItem(new FarmPlot(100.00));
    }

}
