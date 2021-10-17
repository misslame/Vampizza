public class FarmPlot : Structure, Item {
    public FarmPlot(double price) : base(price) {}
    public FarmPlot() : base(100) {}

    public string GetDescription() {
        return "a structure";
    }

    public string GetImageURL() {
        return "Crops/crop_base";
    }
    public override string GetGameObjectURL(){
        return "StructurePrefabs/Crop/Crop";
    }

    public override string GetTileURL(){
        return "TileSprites/crop_base";
    }

    public void OnAction() {
        throw new System.NotImplementedException();
    }

    public override string ToString() {
        return "Farm Plot";
    }

    // public int getCurrentStage(){
    //     StructureController controller = this.GetTile().gameObject.GetComponent<StructureController>();
    //     return controller.getCurrentStage();
    // }

    // public int getTotalStages(){
    //     StructureController controller = this.GetTile().gameObject.GetComponent<StructureController>();
    //     return controller.getTotalStages();
    // }

}
