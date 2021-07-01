
public class Wheat : Resource, Item {

    public Wheat(int quantity) : base(quantity) { }
    public string GetDescription() {
        return "a primitive resource thats gathered.";
    }

    public string GetImageURL() {
        return "wheat";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Wheat";
    }
}