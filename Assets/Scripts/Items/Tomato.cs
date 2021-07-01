
public class Tomato : Resource, Item {

    public Tomato(int quantity) : base(quantity) { }
    public string GetDescription() {
        return "a primitive resource thats gathered.";
    }

    public string GetImageURL() {
        return "tomato";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Tomato";
    }
}
