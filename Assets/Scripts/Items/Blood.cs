public class Blood : Resource, Item {

    public Blood(int quantity) : base(quantity) { }
    public string GetDescription() {
        return "a primitive resource thats gathered.";
    }

    public string GetImageURL() {
        return "blood";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Blood";
    }
}