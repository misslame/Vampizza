using System.Collections.Generic;


public class ItemInventory : Dictionary<string, Item> {
    public void Add(Item value, string keyPrefix) {
        base.Add((keyPrefix + Count.ToString()), value);
    }


    public void Init() {

        Add(new Wheat(3), "res");
        Add(new Tomato(2), "res");
        Add(new Blood(69), "res");
        Add(new TownHome(200), "stc");
        Add(new FarmPlot(100.00), "stc");
    }

}
