public class TownHome : Structure, Item {

    public TownHome(double price) : base(price) { }

    public string GetDescription() {
        return "a structure";
    }

    public string GetImageURL() {
        return "Structures/house";
    }

    public override string GetGameObjectURL(){
        return "";
    }
    
    public override string GetTileURL(){
        return "TileSprites/house";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Town Home";
    }
}
