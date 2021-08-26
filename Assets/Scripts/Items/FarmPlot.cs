public class FarmPlot : Structure, Item {
    public FarmPlot(double price) : base(price) { }

    public string GetDescription() {
        return "a structure";
    }

    public string GetImageURL() {
        return "Crops/crop_base";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Farm Plot";
    }
}
