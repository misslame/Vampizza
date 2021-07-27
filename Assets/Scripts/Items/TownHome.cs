public class TownHome : Structure, Item {

    public override int cost { get { return 200; } }

    public TownHome(double price) : base(price) { }

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
        return "Town Home";
    }
}
