/*
 * ItemHierarchy.cs
 * Class hierarchy for items that can be stored in inventory.
 * The two item types are structures and resources.
 */

using UnityEngine;
using UnityEngine.Tilemaps;
public interface Item {

    public abstract string GetDescription();
    public abstract string GetImageURL();
    public abstract void OnAction();

}

public abstract class Resource {
    private int quantity;

    public Resource() {
        quantity = 1;
    }

    public Resource(int q) {
        quantity = q;
    }

    public abstract override string ToString();
    
    public int GetQuantity() {
        return quantity;
    }

    public void IncreaseQuantity() {
        quantity = quantity + 1;
    }

    public void IncreaseQuantity(int amount) {
        quantity = quantity + amount;
    }
}

public abstract class Structure {
    private Tile tile;

    private uint level;

    private double price;

    public Structure(double p) {
        price = p;
        level = 1;

        // calling Resources.Load for every structure is really inefficient and bad, must fix later. right now is OK
        tile = (Tile)Resources.Load(GetTileURL());
    }
    public abstract override string ToString();
    public abstract string GetTileURL();
    public abstract string GetGameObjectURL();
    public double GetPrice() {
        return price;
    }
    

    public void DestroyGameObject(){
        GameObject.Destroy(tile.gameObject);
    }

    public void LoadGameObject(){
        tile.gameObject = Resources.Load(GetGameObjectURL()) as GameObject;
    }

    public void ChangePrice(float newPrice) {
        price = newPrice;
    }

    
    public uint GetLevel() {
        return level;
    }

    public Tile GetTile() {
        return tile;
    }
}



