public class Home : Structure, Item {

    public Home(double price) : base(price) { }
    public string GetDescription() {
        return "a structure";
    }

    public string GetImageURL() {
        return "Structures/house";
    }

    public override string GetTileURL(){
        return "TileSprites/house";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Home";
    }
}
