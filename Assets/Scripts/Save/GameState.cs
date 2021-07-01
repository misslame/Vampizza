using System.IO;
using UnityEngine;

public static class GameState {
    public const string VERSION_1_0_0 = "1.0.0";
    public const string SAVE_VERSION = VERSION_1_0_0;
    private static PlayerData playerData = new PlayerData();

    public static void LoadState(string filename) {
        Debug.Log("Loading from " + filename + "...");

        if (!File.Exists(filename)) {
            Debug.Log("Save file not found");
            return;
        }

        FileStream saveFile = File.Open(filename, FileMode.Open);
        BinaryReader reader = new BinaryReader(saveFile);

        playerData.Deserialize(reader);

        Debug.Log("Load successful");
    }

    public static void SaveState(string filename) {
        Debug.Log("Saving to " + filename + "...");

        FileStream saveFile = File.Open(filename, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(saveFile);

        playerData.Serialize(writer);
    }

    public static PlayerData GetPlayerData() {
        return playerData;
    }
}
