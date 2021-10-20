using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : List<Item> {
    public void AddItem(Item item) {
        // ideally we would like to avoid a search every time an item is added
        int itemIndex = base.FindIndex(x => x.GetType() == item.GetType());

        if (itemIndex == -1){
            base.Add(item);
        } else {
            Item existingItem = base[itemIndex];
            if (existingItem is Resource){
                ((Resource)existingItem).IncreaseQuantity(((Resource)item).GetQuantity());
            }
            else if (existingItem is Structure){
                // I'm not sure what should happen here
            }
        }
    }


    public void Init() {
        AddItem(new Wheat(3));
        AddItem(new Wheat(3));
        AddItem(new Tomato(2));
        AddItem(new Blood(69));
        AddItem(new TownHome());
        AddItem(new FarmPlot());
    }

}
