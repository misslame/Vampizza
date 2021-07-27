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

    // ew
    public abstract int cost {get;}

    // initial cost is a constant, is this really needed?
    private double price;

    public Structure(double p) {
        price = p;
    }
    public abstract override string ToString();

    public double GetPrice() {
        return price;
    }

    public void ChangePrice(float newPrice) {
        price = newPrice;
    }
}



