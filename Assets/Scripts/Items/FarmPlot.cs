public class FarmPlot : Structure, Item {

    public const int cost = 100;

    public FarmPlot(double price) : base(price) { }

    public string GetDescription() {
        return "a structure";
    }

    public string GetImageURL() {
        return "crop";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Farm Plot";
    }
}
