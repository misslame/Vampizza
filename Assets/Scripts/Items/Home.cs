public class Home : Structure, Item {

    // Home doesn't have cost and should not be able to be bought (?)
    public override int cost { get { return 0; } }
    public Home(double price) : base(price) { }
    public string GetDescription() {
        return "a structure";
    }

    public string GetImageURL() {
        return "house";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Home";
    }
}
